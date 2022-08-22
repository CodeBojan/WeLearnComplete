import {
  MeActionKind,
  MeContext,
  MeInvalidationContext,
  initialMeState,
  meReducer,
} from "../store/me-store";
import { apiAccountsMe, apiGetFetcher, getApiRouteCacheKey } from "../util/api";
import { useEffect, useReducer } from "react";
import useSWR, { mutate } from "swr";

import { AppSession } from "../types/auth";
import BottomNav from "../components/molecules/bottom-nav";
import { ComponentProps } from "../types/components";
import { GetAccountDto } from "../types/api";
import LoadingAuth from "../components/auth/loading-auth";
import Navbar from "../components/molecules/navbar";
import Sidebar from "../components/molecules/sidebar";
import { useAppSession } from "../util/auth";
import { useState } from "react";

const getAccountMeCacheKey = (session: AppSession) => {
  return getApiRouteCacheKey(apiAccountsMe, session);
};

export interface LayoutProps extends ComponentProps {}

export default function Layout({ children, ...props }: LayoutProps) {
  const { data: session, status } = useAppSession();
  const [isSideBarOpen, setIsSidebarOpen] = useState(false);

  const [meState, meDispatch] = useReducer(meReducer, initialMeState);

  const [invalidateMe] = useState(() => () => {
    mutate(getAccountMeCacheKey(session));
  });

  const { data: me, error } = useSWR<GetAccountDto>(
    getAccountMeCacheKey(session),
    apiGetFetcher
  );

  useEffect(() => {
    if (!me) return;
    meDispatch({ type: MeActionKind.SET_ME, payload: me });
  }, [me]);

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
              <Navbar
                onDrawerToggle={() => {
                  setIsSidebarOpen(!isSideBarOpen);
                }}
              />
              <Sidebar
                isOpen={isSideBarOpen}
                onTryClose={() => setIsSidebarOpen(false)}
              />
              <div className="min-h-screen flex flex-col items-center justify-center">
                {children}
              </div>
            </MeInvalidationContext.Provider>
            <BottomNav />
          </MeContext.Provider>
        </div>
      )}
    </div>
  );
}

export const defaultGetLayout = (page: React.ReactElement) => {
  return <Layout>{page}</Layout>;
};
