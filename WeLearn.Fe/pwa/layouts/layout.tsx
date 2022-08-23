import { GetAccountDto, GetUnreadNotificationsDto } from "../types/api";
import {
  MeActionKind,
  MeContext,
  MeInvalidationContext,
  initialMeState,
  meReducer,
} from "../store/me-store";
import {
  NotificationsActionKind,
  NotificationsContext,
  NotificationsInvalidationContext,
  initialNotificationsState,
  notificationsReducer,
} from "../store/notifications-store";
import {
  apiAccountsMe,
  apiGetFetcher,
  apiNotificationsMeUnread,
  getApiRouteCacheKey,
} from "../util/api";
import { useEffect, useReducer } from "react";
import useSWR, { mutate } from "swr";

import { AppSession } from "../types/auth";
import BottomNav from "../components/molecules/bottom-nav";
import { ComponentProps } from "../types/components";
import LoadingAuth from "../components/auth/loading-auth";
import Navbar from "../components/molecules/navbar";
import RightSideBar from "../components/molecules/right-side-bar";
import Sidebar from "../components/molecules/sidebar";
import { useAppSession } from "../util/auth";
import { useState } from "react";

const getAccountMeCacheKey = (session: AppSession) => {
  return getApiRouteCacheKey(apiAccountsMe, session);
};

const getNotificationsMeUnreadCacheKey = (session: AppSession) => {
  return getApiRouteCacheKey(apiNotificationsMeUnread, session);
};

export interface LayoutProps extends ComponentProps {}

export default function Layout({ children, ...props }: LayoutProps) {
  const { data: session, status } = useAppSession();
  const [isSideBarOpen, setIsSidebarOpen] = useState(false);

  // me
  const [meState, meDispatch] = useReducer(meReducer, initialMeState);
  const [invalidateMe] = useState(() => () => {
    mutate(getAccountMeCacheKey(session));
  });
  const { data: me, error } = useSWR<GetAccountDto>(
    getAccountMeCacheKey(session),
    apiGetFetcher
  );
  // end me

  // unread notifications
  const [notificationsState, notificationsDispatch] = useReducer(
    notificationsReducer,
    initialNotificationsState
  );
  const [invalidateNotifications] = useState(() => () => {
    mutate(getNotificationsMeUnreadCacheKey(session));
  });
  const { data: unreadDto, error: unreadCountError } =
    useSWR<GetUnreadNotificationsDto>(
      getNotificationsMeUnreadCacheKey(session),
      apiGetFetcher
    );
  // end unread notifications

  useEffect(() => {
    if (!me) return;
    meDispatch({ type: MeActionKind.SET_ME, payload: me });
  }, [me]);

  useEffect(() => {
    if (!unreadDto) return;
    notificationsDispatch({
      type: NotificationsActionKind.SET_UNREAD_NOTIFICATION_COUNT,
      unreadCount: unreadDto.unread ?? 0,
    });
  }, [unreadDto]);

  return (
    <div className="layout">
      {!session || error ? (
        <LoadingAuth error={error}></LoadingAuth>
      ) : (
        <div>
          <MeContext.Provider value={{ state: meState, dispatch: meDispatch }}>
            <MeInvalidationContext.Provider
              value={{ meInvalidate: invalidateMe }}
            >
              <NotificationsContext.Provider
                value={{
                  state: notificationsState,
                  dispatch: notificationsDispatch,
                }}
              >
                <NotificationsInvalidationContext.Provider
                  value={{ notificationsInvalidate: invalidateNotifications }}
                >
                  <Navbar
                    onDrawerToggle={() => {
                      setIsSidebarOpen(!isSideBarOpen);
                    }}
                  />
                  <Sidebar
                    isOpen={isSideBarOpen}
                    onTryClose={() => setIsSidebarOpen(false)}
                  />
                  <div className="min-h-screen w-full flex flex-row items-start justify-center">
                    <div className="min-h-screen grow flex-col items-center justify-center">
                      {children}
                    </div>
                    <div className="min-h-screen flex flex-row justify-start">
                      <RightSideBar />
                    </div>
                  </div>
                  <BottomNav />
                </NotificationsInvalidationContext.Provider>
              </NotificationsContext.Provider>
            </MeInvalidationContext.Provider>
          </MeContext.Provider>
        </div>
      )}
    </div>
  );
}

export const defaultGetLayout = (page: React.ReactElement) => {
  return <Layout>{page}</Layout>;
};
