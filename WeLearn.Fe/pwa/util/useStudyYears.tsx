import { GetStudyYearDto } from "../types/api";
import { apiStudyYears } from "./api";
import { usePagedData } from "./usePagedData";
import { useState } from "react";

export default function useStudyYears({
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
  } = usePagedData<GetStudyYearDto>({
    pageSize,
    url: apiStudyYears,
    queryParams: {
      isFollowing: followingOnly.toString(),
    },
  });

  return {
    studyYears: entities,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
    hasMore,
  };
}
