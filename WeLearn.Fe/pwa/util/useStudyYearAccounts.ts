import { GetAccountDto, GetAccountDtoPagedResponseDto } from "../types/api";
import {
  apiGetFetcher,
  apiStudyYearAccounts,
  getApiSWRInfiniteKey,
  processSWRInfiniteData,
} from "./api";

import { useAppSession } from "./auth";
import { useSWREffectHook } from "./useSWREffectHook";
import useSWRInfinite from "swr/infinite";
import { useState } from "react";

export default function useStudyYearAccounts({
  studyYearId,
}: {
  studyYearId: string;
}) {
  const { data: session } = useAppSession();
  const [pageSize, setPageSize] = useState(5);
  const [studyYearAccounts, setStudyYearAccounts] = useState<
    GetAccountDto[] | null | undefined
  >();

  const getKey = getApiSWRInfiniteKey({
    url: apiStudyYearAccounts(studyYearId),
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

  return { studyYearAccounts, size, setSize, isLoadingMore, isReachingEnd };
}
