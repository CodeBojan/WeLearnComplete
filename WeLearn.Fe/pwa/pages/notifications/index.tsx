import * as timeago from "timeago.js";

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
import CustomInfiniteScroll from "../../components/molecules/custom-infinite-scroll";
import Link from "next/link";
import NotificationBell from "../../components/atoms/notification-bell";
import TimeAgo from "timeago-react";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import sr from "timeago.js/lib/lang/sr";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";
import { useContext } from "react";
import useNotifications from "../../util/useNotifications";

// TODO extract to component
// timeago.register("sr", sr);

const Notifications: AppPageWithLayout = () => {
  const locale = "sr";
  const { data: session } = useAppSession();
  const notificationsContext = useContext(NotificationsContext);
  const invalidateNotificationsContext = useContext(
    NotificationsInvalidationContext
  );

  // TODO skeletonize

  // TODO check that the invalidateNotificationtext is called - currently the unread count doesnt update until refresh
  // TODO remove first HTML tag/get its content ------------------ IMPORTANT

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
                  className="flex flex-row justify-between items-center p-4 rounded-lg border-l-4 border-r-2 shadow-md border-slate-200"
                >
                  <div className="flex flex-col w-full gap-y-2">
                    <div className="flex items-center gap-x-2">
                      {!notification.isRead && (
                        <MdCircle className="text-xl text-red-500" />
                      )}
                      <span className="text-2xl font-bold ">
                        {notification.title}
                      </span>
                    </div>
                    <div className="font-semibold">
                      {notification.body}
                      {!notification.body?.endsWith(".") ? "..." : ""}{" "}
                    </div>
                    <Link href={notification.uri ?? ""}>
                      <a className="flex flex-row gap-x-2 items-center">
                        <MdOpenInNew className="text-2xl text-primary" />
                        <div>
                          {notification.uri &&
                            new URL(notification.uri ?? "").hostname}
                        </div>
                      </a>
                    </Link>
                    <div>
                      updated{" "}
                      <TimeAgo
                        datetime={notification.updatedDate!}
                        locale={locale}
                      />
                    </div>
                    <div>
                      created{" "}
                      <TimeAgo
                        datetime={notification.createdDate!}
                        locale={locale}
                      />
                    </div>
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
      </CustomInfiniteScroll>
    </TitledPageContainer>
  );
};

Notifications.getLayout = defaultGetLayout;

export default Notifications;
