import { AppSessionValue, AppSesssionUser } from "../types/auth";
import { signIn, useSession } from "next-auth/react";

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

export const isStudyYearAdmin = (
  user: AppSesssionUser,
  studyYearId: string
) => {
  if (checkIsSystemAdmin(user)) return true;
  if (user.studyYearAdmin?.includes(studyYearId)) return true;

  return false;
};

export const isOnlyStudyYearAdmin = (
  user: AppSesssionUser,
  studyYearId: string
) => {
  console.log('user.studyYearAdmin', user.studyYearAdmin)
  console.log('studyYearId', studyYearId)
  return user.studyYearAdmin?.includes(studyYearId);
};

export const isCourseAdmin = (
  user: AppSesssionUser,
  courseId: string,
  studyYearId?: string | null | undefined
) => {
  if (checkIsSystemAdmin(user)) return true;
  if (studyYearId && isOnlyStudyYearAdmin(user, studyYearId)) return true;
  if (user.courseAdmin?.includes(courseId)) return true;

  return false;
};

export const isOnlyCourseAdmin = (user: AppSesssionUser, courseId: string) => {
  return user.courseAdmin?.includes(courseId);
};

export const checkIsSystemAdmin = (user: AppSesssionUser) => {
  if (user.roles.includes(roles.systemAdmin)) return true;

  return false;
};

export const roles = {
  systemAdmin: "Admin",
  user: "User",
};
