import {
  GetStudyYearNoticeDto,
  GetStudyYearNoticeDtoPagedResponseDto,
} from "../types/api";
import {
  apiGetFetcher,
  apiStudyYearNotices,
  getApiSWRInfiniteKey,
  processSWRInfiniteData,
} from "./api";

import { useAppSession } from "./auth";
import { usePagedData } from "./usePagedData";
import { useSWREffectHook } from "./useSWREffectHook";
import useSWRInfinite from "swr/infinite";
import { useState } from "react";

export default function useStudyYearNotices({
  studyYearId,
}: {
  studyYearId: string;
}) {
  const [pageSize, setPageSize] = useState(5);
  const { entities, size, setSize, isLoadingMore, isReachingEnd, hasMore } =
    usePagedData<GetStudyYearNoticeDto>({
      pageSize,
      url: apiStudyYearNotices(studyYearId),
    });

  return {
    studyYearNotices: entities,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    hasMore,
  };
}
