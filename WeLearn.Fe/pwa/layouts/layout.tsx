import "tippy.js/dist/tippy.css";

import * as timeago from "timeago.js";

import {
  CommentsInvalidationActionKind,
  CommentsInvalidationContext,
  commentsInvalidationReducer,
  initialCommentsInvalidationState,
} from "../store/comments-invalidation-context";
import {
  CommentsModalContext,
  commentsModalReducer,
  initialCommentsModalState,
} from "../store/comments-modal-store";
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
import CommentsModal from "../components/molecules/comments-modal";
import { ComponentProps } from "../types/components";
import LoadingAuth from "../components/auth/loading-auth";
import Navbar from "../components/molecules/navbar";
import RightSideBar from "../components/molecules/right-side-bar";
import Sidebar from "../components/molecules/sidebar";
import sr from "timeago.js/lib/lang/sr";
import { useAppSession } from "../util/auth";
import { useState } from "react";

timeago.register("sr", sr);

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
  const [invalidateMe, setInvalidateMe] = useState(() => () => {
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
  const [invalidateNotifications, setInvalidateNotifications] = useState(
    () => () => {
      mutate(getNotificationsMeUnreadCacheKey(session));
    }
  );
  const { data: unreadDto, error: unreadCountError } =
    useSWR<GetUnreadNotificationsDto>(
      getNotificationsMeUnreadCacheKey(session),
      apiGetFetcher
    );
  // end unread notifications

  const [commentsModalState, commentsModalDispatcher] = useReducer(
    commentsModalReducer,
    initialCommentsModalState
  );

  const [commentsInvalidationState, commentsInvalidationDispatcher] =
    useReducer(commentsInvalidationReducer, initialCommentsInvalidationState);

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

  useEffect(() => {
    if (!session) return;

    // TODO check when session is refreshed that the new mutate is set
    setInvalidateMe(() => () => {
      mutate(getAccountMeCacheKey(session));
    });
    setInvalidateNotifications(() => () => {
      mutate(getNotificationsMeUnreadCacheKey(session));
    });
  }, [session]);

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
                  <CommentsModalContext.Provider
                    value={{
                      state: commentsModalState,
                      dispatch: commentsModalDispatcher,
                    }}
                  >
                    <CommentsInvalidationContext.Provider
                      value={{
                        commentsInvalidationState: commentsInvalidationState,
                        commentsInvalidate: () =>
                          commentsInvalidationDispatcher({
                            type: CommentsInvalidationActionKind.INVALIDATE,
                          }),
                      }}
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
                        <div className="min-h-screen w-full grow flex-col items-center justify-center">
                          {children}
                        </div>
                        <div className="min-h-screen flex flex-row justify-start">
                          <RightSideBar />
                        </div>
                      </div>
                      <BottomNav />
                      <CommentsModal />
                    </CommentsInvalidationContext.Provider>
                  </CommentsModalContext.Provider>
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
