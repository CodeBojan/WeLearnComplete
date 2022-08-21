import {
  GetCourseDto,
  GetCourseDtoPagedResponseDto,
  GetFollowedCourseDto,
  PostFollowedCourseDto,
} from "../../types/api";
import {
  apiCourses,
  apiFollowedCourses,
  apiMethodFetcher,
  apiPagedGetFetcher,
  getPagedApiRouteCacheKey,
} from "../../util/api";
import { useEffect, useState } from "react";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import FavoritableContainer from "../../components/containers/favoritable-container";
import FavoriteInfo from "../../components/molecules/favorite-info";
import FavoritesContainer from "../../components/containers/favorites-container";
import { MdSubject } from "react-icons/md";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";

const Courses: AppPageWithLayout = () => {
  const { data: session, status } = useAppSession();
  const [courses, setCourses] = useState<GetCourseDto[]>([]);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [page, setPage] = useState(1);

  // TODO useSWRInfinite

  const cacheKey = getPagedApiRouteCacheKey(
    apiCourses,
    session,
    page,
    itemsPerPage
  );

  const { data: pagedStudyYears, error } = useSWR<GetCourseDtoPagedResponseDto>(
    cacheKey,
    apiPagedGetFetcher
  );

  useEffect(() => {
    if (!pagedStudyYears || !pagedStudyYears.data) return;
    setCourses(pagedStudyYears.data);
  }, [pagedStudyYears]);

  return (
    <TitledPageContainer icon={<MdSubject />} title={"Courses"}>
      <FavoritesContainer>
        {courses.map((course) => (
          <FavoritableContainer key={course.id}>
            <div className="flex flex-row gap-x-8">
              <div>[{course.code} - {course.shortName}]</div>
              <div>{course.fullName}</div>
            </div>
            <FavoriteInfo
              isFollowing={course.isFollowing}
              followerCount={course.followingCount}
              onUnfollow={() => {
                apiMethodFetcher(
                  apiFollowedCourses,
                  session.accessToken,
                  "DELETE",
                  {
                    accountId: session.user.id,
                    courseId: course.id,
                  } as PostFollowedCourseDto
                ).then((res) => {
                  const resDto = res as GetFollowedCourseDto;
                  mutate(cacheKey);
                  toast(`Unfollowed ${course.shortName}`, { type: "success" });
                });
              }}
              onFollow={() => {
                apiMethodFetcher(
                  apiFollowedCourses,
                  session.accessToken,
                  "POST",
                  {
                    accountId: session.user.id,
                    courseId: course.id,
                  } as PostFollowedCourseDto
                ).then((res) => {
                  const resDto = res as GetFollowedCourseDto;
                  mutate(cacheKey);
                  toast(`Followed ${course.shortName}`, { type: "success" });
                });
              }}
            />
          </FavoritableContainer>
        ))}
      </FavoritesContainer>
    </TitledPageContainer>
  );
};

Courses.getLayout = defaultGetLayout;

export default Courses;
