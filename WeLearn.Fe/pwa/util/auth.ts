import { signIn, useSession } from "next-auth/react";

import { AppSessionValue } from "../types/auth";

export const signOutUrl = "/auth/begin-signout";

export function signInUtil() {
  signIn("identityServer");
}

export function useAppSession(): AppSessionValue {
  return { ...useSession() } as AppSessionValue;
}

export const getIsIssuer = () => process.env.IS_ISSUER;
export const getClientId = () => process.env.IS_CLIENT_ID;
export const getClientSecret = () => process.env.IS_CLIENT_SECRET;
export const getScopes = () => process.env.IS_SCOPES;
