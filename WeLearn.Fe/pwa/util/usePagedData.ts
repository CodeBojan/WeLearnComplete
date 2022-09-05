import {
  Entity,
  PageDtos,
  apiGetFetcher,
  getApiSWRInfiniteKey,
  processSWRInfiniteData,
} from "./api";

import { useAppSession } from "./auth";
import { useSWREffectHook } from "./useSWREffectHook";
import useSWRInfinite from "swr/infinite";
import { useState } from "react";

export function usePagedData<
  TEntity extends Entity,
  TPagedEntity extends PageDtos<TEntity> = PageDtos<TEntity>
>({
  url,
  pageSize,
  queryParams,
}: {
  url: string;
  pageSize: number;
  queryParams?: Record<string, string>;
}) {
  const { data: session } = useAppSession();
  const [entities, setEntities] = useState<TEntity[] | null>();

  const getKey = getApiSWRInfiniteKey({
    ...queryParams,
    url: url,
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
  } = useSWRInfinite<TPagedEntity>(getKey, apiGetFetcher, {
    revalidateAll: true,
  });

  const { isLoadingMore, isReachingEnd } = processSWRInfiniteData<
    TEntity,
    TPagedEntity
  >(size, pageSize, isValidating, error, pageDtos);

  useSWREffectHook<TEntity>(pageDtos, setEntities);

  return {
    entities,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
  };
}
