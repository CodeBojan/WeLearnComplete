import { GetCommentDto, GetCommentDtoPagedResponseDto } from "../types/api";
import {
  apiContentComments,
  apiGetFetcher,
  getApiSWRInfiniteKey,
  processSWRInfiniteData,
} from "./api";

import { useAppSession } from "./auth";
import { useSWREffectHook } from "./useSWREffectHook";
import useSWRInfinite from "swr/infinite";
import { useState } from "react";

export default function useContentComments({
  contentId,
}: {
  contentId: string | null | undefined;
}) {
  const { data: session } = useAppSession();
  const [pageSize, setPageSize] = useState(5); // TODO

  const [comments, setComments] = useState<
    GetCommentDto[] | null | undefined
  >();

  const getKey = getApiSWRInfiniteKey({
    url: !contentId ? null : apiContentComments(contentId),
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
  } = useSWRInfinite<GetCommentDtoPagedResponseDto>(getKey, apiGetFetcher, {
    revalidateAll: true,
  });

  const { isLoadingMore, isReachingEnd } = processSWRInfiniteData(
    size,
    pageSize,
    isValidating,
    error,
    pageDtos
  );

  useSWREffectHook<GetCommentDto>(pageDtos, setComments);

  return { comments, size, setSize, isLoadingMore, isReachingEnd, mutate, isValidating };
}
