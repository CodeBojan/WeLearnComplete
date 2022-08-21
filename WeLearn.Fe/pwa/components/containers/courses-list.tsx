import {
  GetCourseDto,
  GetFollowedCourseDto,
  PostFollowedCourseDto,
} from "../../types/api";
import { apiFollowedCourses, apiMethodFetcher } from "../../util/api";

import CourseFollowInfo from "../molecules/course-follow-info";
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
