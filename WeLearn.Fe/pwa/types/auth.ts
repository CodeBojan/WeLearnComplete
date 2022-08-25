import { DefaultUser, Session } from "next-auth";
import {
  SessionContextValue,
  UseSessionOptions,
  useSession,
} from "next-auth/react";

export interface AppSession extends Session {
  accessToken: string;
  idToken: string;
  error: string;
  user: AppSesssionUser;
}

export declare type AppSesssionUser = DefaultUser &
  Record<string, unknown> & {
    id: string;
    roles: string[];
    studyYearAdmin: string[] | null;
    courseAdmin: string[] | null;
  };

export declare type AppSessionValue = SessionContextValue<boolean> & {
  data: AppSession;
};
