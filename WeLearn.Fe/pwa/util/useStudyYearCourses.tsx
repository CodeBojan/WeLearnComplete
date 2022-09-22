import { GetCourseDto } from "../types/api";
import { apiCourses } from "./api";
import { usePagedData } from "./usePagedData";
import { useState } from "react";

export default function useStudyYearCourses({
  followingOnly,
  studyYearId,
}: {
  followingOnly: boolean;
  studyYearId: string;
}) {
  const [pageSize, setPageSize] = useState(20); // TODO page size
  const {
    entities,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
    hasMore,
  } = usePagedData<GetCourseDto>({
    pageSize,
    url: apiCourses,
    queryParams: {
      isFollowing: followingOnly.toString(),
      studyYearId: studyYearId,
    },
  });

  return {
    courses: entities,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
    hasMore,
  };
}
