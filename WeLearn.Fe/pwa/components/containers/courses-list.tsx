import {
  GetCourseDto,
  GetFollowedCourseDto,
  PostFollowedCourseDto,
} from "../../types/api";
import { apiFollowedCourses, apiMethodFetcher } from "../../util/api";

import FavoritableContainer from "./favoritable-container";
import FavoriteInfo from "../molecules/favorite-info";
import FavoritesContainer from "./favorites-container";
import { mutate } from "swr";
import router from "next/router";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";

export default function CoursesContainer({
  courses,
  onMutate,
}: {
  courses: GetCourseDto[];
  onMutate?: () => void;
}) {
  const { data: session, status } = useAppSession();

  return (
    <FavoritesContainer>
      {courses.map((course) => (
        <FavoritableContainer key={course.id}>
          <div
            className="flex flex-row gap-x-8 cursor-pointer"
            onClick={() => router.push(`/course/${course.id}`)}
          >
            <div>
              [{course.code} - {course.shortName}]
            </div>
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
                onMutate && onMutate();
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
                onMutate && onMutate();
                toast(`Followed ${course.shortName}`, { type: "success" });
              });
            }}
          />
        </FavoritableContainer>
      ))}
    </FavoritesContainer>
  );
}
