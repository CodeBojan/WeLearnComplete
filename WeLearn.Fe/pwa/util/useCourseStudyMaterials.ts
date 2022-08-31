import {
  GetAccountDto,
  GetStudyMaterialDto,
  GetStudyMaterialDtoPagedResponseDto,
} from "../types/api";
import {
  apiGetFetcher,
  apiStudyMaterialsCourse,
  getApiSWRInfiniteKey,
  processSWRInfiniteData,
} from "./api";

import { useAppSession } from "./auth";
import { useSWREffectHook } from "./useSWREffectHook";
import useSWRInfinite from "swr/infinite";
import { useState } from "react";

// TODO replace loaders
// TODO abstract further - provide two generic arguments and url
export default function useCourseStudyMaterials({
  courseId,
}: {
  courseId: string;
}) {
  const { data: session } = useAppSession();
  const [pageSize, setPageSize] = useState(2); // TODO
  const [studyMaterials, setStudyMaterials] = useState<
    GetStudyMaterialDto[] | null | undefined
  >();

  const getKey = getApiSWRInfiniteKey({
    url: apiStudyMaterialsCourse(courseId),
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
  } = useSWRInfinite<GetStudyMaterialDtoPagedResponseDto>(
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

  useSWREffectHook<GetAccountDto>(pageDtos, setStudyMaterials);

  return { studyMaterials, size, setSize, isLoadingMore, isReachingEnd, mutate };
}
