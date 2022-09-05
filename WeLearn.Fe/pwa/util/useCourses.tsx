import { GetCourseDto, GetStudyYearDto } from "../types/api";
import { apiCourses, apiStudyYears } from "./api";

import { usePagedData } from "./usePagedData";
import { useState } from "react";

export default function useCourses({
  followingOnly,
}: {
  followingOnly: boolean;
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
