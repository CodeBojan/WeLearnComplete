import { GetContentDto, GetNotificationDto } from "../types/api";
import { apiFeed, apiNotificationsMe } from "./api";

import { usePagedData } from "./usePagedData";
import { useState } from "react";

export default function useNotifications({}: {}) {
  const [pageSize, setPageSize] = useState(10); // TODO
  const {
    entities,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
    hasMore,
  } = usePagedData<GetNotificationDto>({
    pageSize,
    url: apiNotificationsMe,
  });

  return {
    notifications: entities,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
    hasMore,
  };
}
