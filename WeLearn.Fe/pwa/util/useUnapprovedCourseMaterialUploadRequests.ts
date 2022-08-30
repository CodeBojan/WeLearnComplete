import {
  GetCourseMaterialUploadRequestDto,
  GetCourseMaterialUploadRequestDtoPagedResponseDto,
} from "../types/api";
import {
  apiGetFetcher,
  apiUnapprovedStudyMaterialsCourse,
  getApiSWRInfiniteKey,
  processSWRInfiniteData,
} from "./api";

import { useAppSession } from "./auth";
import { useSWREffectHook } from "./useSWREffectHook";
import useSWRInfinite from "swr/infinite";
import { useState } from "react";

export default function useUnapprovedCourseMaterialUploadRequests({
  courseId,
}: {
  courseId: string;
}) {
  const { data: session } = useAppSession();
  const [pageSize, setPageSize] = useState(5); // TODO
  const [requests, setRequests] = useState<
    GetCourseMaterialUploadRequestDto[] | null | undefined
  >();

  const getKey = getApiSWRInfiniteKey({
    url: apiUnapprovedStudyMaterialsCourse(courseId),
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
  } = useSWRInfinite<GetCourseMaterialUploadRequestDtoPagedResponseDto>(
    getKey,
    apiGetFetcher,
    {
      revalidateAll: true,
    }
  );

  const { isLoadingMore, isReachingEnd } = processSWRInfiniteData(
    size,
    pageSize,
    isValidating,
    error,
    pageDtos
  );

  useSWREffectHook<GetCourseMaterialUploadRequestDto>(pageDtos, setRequests);

  return { requests, size, setSize, isLoadingMore, isReachingEnd };
}
