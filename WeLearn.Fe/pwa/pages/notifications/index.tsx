import {
  GetNotificationDto,
  GetNotificationDtoPagedResponseDto,
  PostNotificationReadStatusDto,
} from "../../types/api";
import { IoMdEye, IoMdEyeOff } from "react-icons/io";
import {
  NotificationsContext,
  NotificationsInvalidationContext,
} from "../../store/notifications-store";
import {
  apiGetFetcher,
  apiMethodFetcher,
  apiNotificationReadStatus,
  apiNotificationsMe,
  getApiSWRInfiniteKey,
  getPagedSearchApiRouteCacheKey,
  processSWRInfiniteData,
} from "../../util/api";
import { useContext, useEffect, useMemo, useRef, useState } from "react";

import { AppPageWithLayout } from "../_app";
import InfiniteScroll from "react-infinite-scroller";
import Link from "next/link";
import { MdOpenInNew } from "react-icons/md";
import NotificationBell from "../../components/atoms/notification-bell";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";
import useSWRInfinite from "swr/infinite";

const Notifications: AppPageWithLayout = () => {
  const { data: session } = useAppSession();
  const notificationsContext = useContext(NotificationsContext);
  const invalidateNotificationsContext = useContext(
    NotificationsInvalidationContext
  );

  // TODO skeletonize

  const [pageSize, setPageSize] = useState(2); // TODO update

  const getKey = getApiSWRInfiniteKey({
    url: apiNotificationsMe,
    session: session,
    pageSize: pageSize,
  });

  const {
    data: pagesDtos,
    error,
    isValidating,
    mutate,
    size,
    setSize,
  } = useSWRInfinite<GetNotificationDtoPagedResponseDto>(
    getKey,
    apiGetFetcher,
    { revalidateAll: true }
  );

  const { isLoadingMore, isReachingEnd } = processSWRInfiniteData(
    size,
    pageSize,
    isValidating,
    error,
    pagesDtos
  );

  const [notifications, setNotifications] = useState<
    GetNotificationDto[] | null
  >(null);

  useEffect(() => {
    if (!pagesDtos) return;

    const notifMap = new Map<string, GetNotificationDto>();
    pagesDtos.forEach((pageDto) => {
      pageDto.data?.forEach((notif) => {
        notif.id && notifMap.set(notif.id, notif);
      });
    });

    setNotifications(Array.from(notifMap.values()));
  }, [pagesDtos]);

  const postNotificationReadStatus = ({
    notifications,
    notification,
    readStatus,
  }: {
    notifications: GetNotificationDto[];
    notification: GetNotificationDto;
    readStatus: boolean;
  }) => {
    if (!notification.id) {
      toast("Notification id is missing", {
        type: "error",
      });
      return;
    }
    apiMethodFetcher(
      apiNotificationReadStatus(notification.id),
      session.accessToken,
      "POST",
      {
        notificationId: notification.id,
        readState: readStatus,
      } as PostNotificationReadStatusDto,
      false
    ).then((res) => {
      const response = res as Response;
      if (response.ok) {
        toast(readStatus ? "Notification read" : "Notification unread", {
          type: "success",
        });

        const newNotification = {
          ...notification,
          isRead: readStatus,
        } as GetNotificationDto;
        const filteredNotifications = notifications.filter(
          (n) => n.id !== notification.id
        );
        const newNotifications = [...filteredNotifications, newNotification];
        mutate(newNotifications);
        invalidateNotificationsContext.notificationsInvalidate();
      } else
        toast(
          readStatus
            ? "Notification read failed"
            : "Notification unread failed",
          {
            type: "error",
          }
        );
    });
  };

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
          {/* TODO show skeleton if notifications is null */}
          {pagesDtos &&
            notifications &&
            notifications.flatMap((notification, notifIndex) => {
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
                    <div>Updated at {notification.updatedDate?.toString()}</div>
                  </div>
                  <div className="flex flex-col justify-center text-2xl gap-y-4">
                    {/* TODO replace with external system image uri - do this based on type */}
                    {notification.imageUri && (
                      <img
                        className="rounded-full h-64"
                        src={notification.imageUri}
                      />
                    )}
                    {notification.isRead ? (
                      <div
                        className="flex flex-row justify-center items-center cursor-pointer"
                        onClick={() => {
                          postNotificationReadStatus({
                            notifications,
                            notification,
                            readStatus: false,
                          });
                        }}
                      >
                        <div className="p-2 rounded-full hover:bg-slate-200 uppercase">
                          MARK UNREAD
                          <IoMdEye className="text-2xl" />
                        </div>
                      </div>
                    ) : (
                      <div
                        className="flex flex-row justify-center items-center cursor-pointer"
                        onClick={() => {
                          postNotificationReadStatus({
                            notifications: notifications,
                            notification: notification,
                            readStatus: true,
                          });
                        }}
                      >
                        <div className="p-2 rounded-full hover:bg-slate-200 uppercase">
                          MARK READ
                          <IoMdEyeOff className="text-2xl" />
                        </div>
                      </div>
                    )}
                  </div>
                </div>
              );
            })}
        </div>
      </InfiniteScroll>
    </TitledPageContainer>
  );
};

Notifications.getLayout = defaultGetLayout;

export default Notifications;
