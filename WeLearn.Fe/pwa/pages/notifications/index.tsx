import {
  GetNotificationDto,
  GetNotificationDtoPagedResponseDto,
} from "../../types/api";
import { IoMdEye, IoMdEyeOff } from "react-icons/io";
import {
  apiGetFetcher,
  apiNotificationsMe,
  getPagedSearchApiRouteCacheKey,
} from "../../util/api";
import { useContext, useEffect, useMemo, useRef, useState } from "react";

import { AppPageWithLayout } from "../_app";
import InfiniteScroll from "react-infinite-scroller";
import Link from "next/link";
import { MdOpenInNew } from "react-icons/md";
import NotificationBell from "../../components/atoms/notification-bell";
import { NotificationsContext } from "../../store/notifications-store";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { useAppSession } from "../../util/auth";
import useSWRInfinite from "swr/infinite";

const Notifications: AppPageWithLayout = () => {
  const { data: session } = useAppSession();
  const notificationsContext = useContext(NotificationsContext);

  const [pageSize, setPageSize] = useState(2);

  const getKey = (
    pageIndex: number,
    previousPageData: GetNotificationDtoPagedResponseDto | null
  ) => {
    const pagedCacheKey = getPagedSearchApiRouteCacheKey(
      apiNotificationsMe,
      session,
      {
        page: (pageIndex + 1).toString(),
        limit: pageSize.toString(),
      }
    );

    if (previousPageData)
      if (pageIndex < (previousPageData.totalPages ?? 0)) {
        return pagedCacheKey;
      } else {
        return null;
      }

    return pagedCacheKey;
  };

  const {
    data: pagesDtos,
    error,
    isValidating,
    mutate,
    size,
    setSize,
  } = useSWRInfinite<GetNotificationDtoPagedResponseDto>(getKey, apiGetFetcher);

  const pages = pagesDtos ? [...pagesDtos] : [];
  const isLoadingInitialData = !pagesDtos && !error;
  const isLoadingMore =
    isLoadingInitialData ||
    (size > 0 && pagesDtos && typeof pagesDtos[size - 1] === "undefined");
  const isEmpty = pagesDtos?.[0]?.data?.length === 0;
  const isReachingEnd =
    isEmpty ||
    (pagesDtos &&
      (pagesDtos[pagesDtos.length - 1]?.data?.length ?? 0) < pageSize);
  const isRefreshing = isValidating && pagesDtos && pagesDtos.length == size;

  return (
    <TitledPageContainer
      icon={
        <NotificationBell
          theme="black"
          notifCount={notificationsContext.state.unreadCount}
          textsize="large"
        />
      }
      title={"Notifications"}
    >
      <InfiniteScroll
        pageStart={1}
        loadMore={() => {
          setSize(size + 1);
        }}
        hasMore={!isLoadingMore && !isReachingEnd}
        loader={
          <div className="loader" key={0}>
            Loading ...
          </div>
        }
      >
        <div className="flex flex-col gap-y-4 w-full my-8">
          {pagesDtos?.flatMap((notifications, pageIndex) =>
            notifications.data?.flatMap((notification, notifIndex) => {
              return (
                <div
                  key={notification.id}
                  className="flex flex-row justify-between items-center p-4 rounded-lg border-l-4 border-r-2 shadow-md border-slate-200"
                >
                  <div className="flex flex-col w-full gap-y-2">
                    <div className="text-2xl font-bold">
                      {notification.title}
                    </div>
                    <div className="font-semibold">{notification.body}</div>
                    <Link href={notification.uri ?? ""}>
                      <a className="flex flex-row gap-x-2 items-center">
                        <MdOpenInNew className="text-2xl text-primary" />
                        <div>{new URL(notification.uri ?? "").hostname}</div>
                      </a>
                    </Link>
                    <div>Created at {notification.createdDate?.toString()}</div>
                    <div>Updated at {notification.createdDate?.toString()}</div>
                  </div>
                  <div className="flex flex-col text-2xl">
                    {/* TODO replace with external system image uri - do this based on type */}
                    {notification.imageUri && (
                      <img
                        className="rounded-full h-64"
                        src={notification.imageUri}
                      />
                    )}
                    {notification.isRead ? (
                      <div className="rounded-full hover:bg-slate-200">
                        <IoMdEye />
                      </div>
                    ) : (
                      <div>
                        <IoMdEyeOff className="text-2xl rounded-full hover:bg-slate-200" />
                      </div>
                    )}
                  </div>
                </div>
              );
            })
          )}
        </div>
      </InfiniteScroll>
    </TitledPageContainer>
  );
};

Notifications.getLayout = defaultGetLayout;

export default Notifications;
