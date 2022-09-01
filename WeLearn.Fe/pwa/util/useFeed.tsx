import { GetContentDto } from "../types/api";
import { apiFeed } from "./api";
import { usePagedData } from "./usePagedData";
import { useState } from "react";

export default function useFeed() {
  const [pageSize, setPageSize] = useState(10); // TODO
  const { entities, size, setSize, isLoadingMore, isReachingEnd, mutate } =
    usePagedData<GetContentDto>({
      pageSize,
      url: apiFeed,
    });

  return {
    feed: entities,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
    hasMore: !isLoadingMore && !isReachingEnd,
  };
}
