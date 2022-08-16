import { signIn } from "next-auth/react";

export const signOutUrl = "/auth/begin-signout";

export function signInUtil() {
    signIn("identityServer");
}