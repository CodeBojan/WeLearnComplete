import {
  SessionContextValue,
  UseSessionOptions,
  useSession,
} from "next-auth/react";

import { Session } from "next-auth";

export interface AppSession extends Session {
  accessToken: string;
  idToken: string;
  error: string;
  user: Record<string, unknown>;
}

export declare type AppSessionValue = SessionContextValue<boolean> & {
  data: AppSession;
};
