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
  user: DefaultUser & Record<string, unknown>;
}

export declare type AppSessionValue = SessionContextValue<boolean> & {
  data: AppSession;
};
