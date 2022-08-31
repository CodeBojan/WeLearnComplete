import {
  GetCourseDto,
  GetFollowedCourseDto,
  PostFollowedCourseDto,
} from "../../types/api";
import { apiFollowedCourses, apiMethodFetcher } from "../../util/api";
import { checkIsCourseAdmin, useAppSession } from "../../util/auth";

import CourseFollowInfo from "../molecules/course-follow-info";
import FavoritableContainer from "./favoritable-container";
import FavoriteInfo from "../molecules/favorite-info";
import FavoritesContainer from "./favorites-container";
import { GrUserAdmin } from "react-icons/gr";
import { mutate } from "swr";
import router from "next/router";
import { toast } from "react-toastify";

export default function CoursesContainer({
  courses,
  onMutate,
}: {
  courses: GetCourseDto[];
  onMutate?: () => void;
}) {
  const { data: session, status } = useAppSession();
  // TODO skeletonized preview if no courses

  return (
    <FavoritesContainer>
      {courses.map((course) => (
        <FavoritableContainer key={course.id}>
          <div
            className="flex flex-row gap-x-8 cursor-pointer items-center"
            onClick={() => router.push(`/course/${course.id}`)}
          >
            {checkIsCourseAdmin(session.user, course.id!, course.studyYearId) && (
              <GrUserAdmin className="text-2xl" />
            )}
            <div>
              [{course.code} - {course.shortName}]
            </div>
            <div>{course.fullName}</div>
          </div>
          <CourseFollowInfo
            courseId={course.id}
            courseShortName={course.shortName}
            followingCount={course.followingCount}
            isFollowing={course.isFollowing}
            onMutate={onMutate}
          />
        </FavoritableContainer>
      ))}
    </FavoritesContainer>
  );
}
