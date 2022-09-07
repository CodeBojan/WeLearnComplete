import {
  GetNotificationDto,
  PostNotificationReadStatusDto,
} from "../../types/api";
import { IoMdEye, IoMdEyeOff } from "react-icons/io";
import { MdCircle, MdOpenInNew } from "react-icons/md";
import {
  NotificationsContext,
  NotificationsInvalidationContext,
} from "../../store/notifications-store";
import { apiMethodFetcher, apiNotificationReadStatus } from "../../util/api";

import { AppPageWithLayout } from "../_app";
import CreatedUpdatedDates from "../../components/molecules/created-updated-dates";
import CustomInfiniteScroll from "../../components/molecules/custom-infinite-scroll";
import Link from "next/link";
import NotificationBell from "../../components/atoms/notification-bell";
import TimeAgo from "timeago-react";
import Tippy from "@tippyjs/react";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";
import { useContext } from "react";
import useNotifications from "../../util/useNotifications";
import { useRouter } from "next/router";
import { useTranslation } from "next-i18next";

const Notifications: AppPageWithLayout = () => {
  const { locale } = useRouter();
  const { data: session } = useAppSession();
  const notificationsContext = useContext(NotificationsContext);
  const invalidateNotificationsContext = useContext(
    NotificationsInvalidationContext
  );

  // TODO skeletonize

  const {
    notifications,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
    hasMore,
  } = useNotifications({});

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
        mutate([{ data: newNotifications }]);
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

  const htmlTagRegex = /(<([^>]+)>)/gi;

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
      <CustomInfiniteScroll
        dataLength={notifications?.length ?? 0}
        next={() => setSize(size + 1)}
        hasMore={hasMore}
      >
        <div className="flex flex-col gap-y-4 w-full my-8">
          {notifications &&
            notifications.flatMap((notification, notifIndex) => {
              return (
                <div
                  key={notification.id}
                  className="flex flex-col items-end md:items-center md:flex-row md:justify-between gap-x-4  p-4 rounded-lg border-l-4 border-r-2 shadow-md border-slate-200"
                >
                  <div className="flex flex-col w-full gap-y-2">
                    <div className="flex items-center gap-x-2">
                      {!notification.isRead && (
                        <MdCircle className="text-xl text-red-500" />
                      )}
                      {notification.imageUri && (
                        <img
                          className="rounded-full h-12 w-12"
                          src={notification.imageUri}
                        />
                      )}
                      <span className="text-2xl font-bold ">
                        {notification.title}
                      </span>
                    </div>
                    <div className="font-semibold">
                      {notification.body?.replace(htmlTagRegex, "")}
                      {!notification.body?.endsWith(".") ? "..." : ""}
                    </div>
                    {notification.uri && (
                      <div className="flex">
                        <Link href={notification.uri}>
                          <a className="flex flex-row gap-x-2 items-center p-1 hover:bg-slate-200 rounded-lg">
                            <MdOpenInNew className="text-2xl text-primary" />
                            <div className="font-semibold">
                              {new URL(notification.uri).hostname}
                            </div>
                          </a>
                        </Link>
                      </div>
                    )}
                    <div className="flex flex-row justify-between items-center">
                      {/* Dates */}
                      <CreatedUpdatedDates
                        entity={notification}
                        locale={locale}
                      />
                      {/* Interaction */}
                      <div className="">
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
                            <div
                              className="p-2 rounded-full hover:bg-slate-200 uppercase text-base
                        text-center whitespace-nowrap"
                            >
                              MARK READ
                            </div>
                          </div>
                        )}
                      </div>
                    </div>
                  </div>
                </div>
              );
            })}
        </div>
      </CustomInfiniteScroll>
    </TitledPageContainer>
  );
};

export async function getStaticProps({ locale }: { locale: string }) {
  return {
    props: {
      ...(await serverSideTranslations(locale, ["common"])),
    },
  };
}

Notifications.getLayout = defaultGetLayout;

export default Notifications;
