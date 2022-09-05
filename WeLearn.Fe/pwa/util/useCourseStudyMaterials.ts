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
import { usePagedData } from "./usePagedData";
import { useSWREffectHook } from "./useSWREffectHook";
import useSWRInfinite from "swr/infinite";
import { useState } from "react";

export default function useCourseStudyMaterials({
  courseId,
}: {
  courseId: string;
}) {
  const { data: session } = useAppSession();
  const [pageSize, setPageSize] = useState(5); // TODO

  const {
    entities: studyMaterials,
    mutate,
    size,
    setSize,
    hasMore,
    isLoadingMore,
    isReachingEnd,
  } = usePagedData<GetStudyMaterialDto>({
    pageSize: pageSize,
    url: apiStudyMaterialsCourse(courseId),
  });

  return {
    studyMaterials,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
    hasMore
  };
}
