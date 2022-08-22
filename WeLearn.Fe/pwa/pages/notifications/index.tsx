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

  const [previousPageData, setPreviousPageData] = useState<
    GetNotificationDto[] | null
  >([]);

  const getKey = (
    pageIndex: number,
    previousPageData: GetNotificationDto[] | null
  ) => {
    console.log(pageIndex);
    const pagedCacheKey = getPagedSearchApiRouteCacheKey(
      apiNotificationsMe,
      session,
      {
        page: (pageIndex + 1).toString(),
        limit: "2", // TODO make this configurable
      }
    );
    console.log("pagedCacheKey", pagedCacheKey);
    const returnedCacheKey = !pagedCacheKey
      ? null
      : previousPageData && !previousPageData.length
      ? null
      : pagedCacheKey;

    console.log("returnedCacheKey", returnedCacheKey);

    return returnedCacheKey;
  };

  const {
    data: pages,
    error,
    isValidating,
    mutate,
    size,
    setSize,
  } = useSWRInfinite<GetNotificationDtoPagedResponseDto>(getKey, apiGetFetcher);

  const lastPageIndex = (pages?.length ?? 0) - 1;
  const lastNotificationIndex = (pages?.[lastPageIndex]?.data?.length ?? 0) - 1;
  console.log("lastPageIndex", lastPageIndex);
  console.log("lastNotificationIndex", lastNotificationIndex);

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
      <div className="flex flex-col gap-y-4 w-full h-screen my-8">
        {pages?.flatMap((notifications, pageIndex) =>
          notifications.data?.flatMap((notification, notifIndex) => {
            const isLast =
              pageIndex === lastPageIndex &&
              notifIndex === lastNotificationIndex;
            console.log(isLast);
            return (
              <div
                key={`${pageIndex}-${notifIndex}`}
                className="flex flex-row justify-between items-center p-4 rounded-lg border-l-4 border-r-2 shadow-md border-slate-200"
              >
                <div className="flex flex-col w-full gap-y-2">
                  <div className="text-2xl font-bold">{notification.title}</div>
                  {/* TODO change depending on notificationType <div className="text-xl">{notification.type}</div> */}
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
                    <div>
                      <IoMdEye />
                    </div>
                  ) : (
                    <div>
                      <IoMdEyeOff />
                    </div>
                  )}
                </div>
              </div>
            );
          })
        )}
      </div>
    </TitledPageContainer>
  );
};

Notifications.getLayout = defaultGetLayout;

export default Notifications;
