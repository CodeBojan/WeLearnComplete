import { MeActionKind, MeContext, initialMeState, meReducer } from "../store/me-store";
import { createContext, useEffect, useReducer } from "react";

import BottomNav from "../components/molecules/bottom-nav";
import { ComponentProps } from "../types/components";
import { GetAccountDto } from "../types/api";
import LoadingAuth from "../components/auth/loading-auth";
import Navbar from "../components/molecules/navbar";
import Sidebar from "../components/molecules/sidebar";
import { apiGetFetcher } from "../util/api";
import { useAppSession } from "../util/auth";
import useSWR from "swr";
import { useState } from "react";

export interface LayoutProps extends ComponentProps {}

export default function Layout({ children, ...props }: LayoutProps) {
  const { data: session, status } = useAppSession();
  const [isSideBarOpen, setIsSidebarOpen] = useState(false);

  const [meState, meDispatch] = useReducer(meReducer, initialMeState);

  const { data: me, error } = useSWR<GetAccountDto>(
    () => (!session ? null : ["/api/Accounts/Me", session.accessToken]),
    apiGetFetcher
  );

  useEffect(() => {
    if (!me) return;
    meDispatch({ type: MeActionKind.SET_ME, payload: me });
  }, [me]);

  return (
    <div className="layout">
      {!session ? (
        <LoadingAuth></LoadingAuth>
      ) : (
        <div>
          <MeContext.Provider value={meState}>
            <Navbar
              onDrawerToggle={() => {
                setIsSidebarOpen(!isSideBarOpen);
              }}
            />
            <Sidebar
              isOpen={isSideBarOpen}
              onTryClose={() => setIsSidebarOpen(false)}
            />
            <div>{children}</div>
            <BottomNav />
          </MeContext.Provider>
        </div>
      )}
    </div>
  );
}
