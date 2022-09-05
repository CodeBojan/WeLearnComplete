import { GetAccountDto, GetAccountDtoPagedResponseDto } from "../types/api";
import {
  apiCourseAccounts,
  apiGetFetcher,
  apiStudyYearAccounts,
  getApiSWRInfiniteKey,
  processSWRInfiniteData,
} from "./api";

import { useAppSession } from "./auth";
import { useSWREffectHook } from "./useSWREffectHook";
import useSWRInfinite from "swr/infinite";
import { useState } from "react";

export default function useCourseAccounts({ courseId }: { courseId: string }) {
  const { data: session } = useAppSession();
  const [pageSize, setPageSize] = useState(10); // TODO
  const [studyYearAccounts, setStudyYearAccounts] = useState<
    GetAccountDto[] | null | undefined
  >();

  const getKey = getApiSWRInfiniteKey({
    url: apiCourseAccounts(courseId),
    pageSize: pageSize,
    session: session,
  });

  const {
    data: pageDtos,
    error,
    isValidating,
    mutate,
    size,
    setSize,
  } = useSWRInfinite<GetAccountDtoPagedResponseDto>(getKey, apiGetFetcher, {
    revalidateAll: true,
  });

  const { isLoadingMore, isReachingEnd } = processSWRInfiniteData(
    size,
    pageSize,
    isValidating,
    error,
    pageDtos
  );

  useSWREffectHook<GetAccountDto>(pageDtos, setStudyYearAccounts);

  return {
    studyYearAccounts,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
  };
}
