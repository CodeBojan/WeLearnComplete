import { AppSession } from "../types/auth";

export const apiBaseUrl = process.env.NEXT_PUBLIC_API;

export function apiRoute(route: string) {
  return `${apiBaseUrl}${route}`;
}

export const apiGetFetcher = (url: string, token: string, ...props: any) =>
  fetch(apiRoute(url), {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((res) => res.json());

export const apiSearchGetFetcher = (
  url: string,
  token: string,
  searchParams: URLSearchParams,
  ...props: any
) => {
  return fetch(`${apiRoute(url)}?` + searchParams, {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((res) => res.json());
};

export const apiPagedGetFetcher = (
  url: string,
  token: string,
  page: number,
  itemsPerPage: number,
  ...props: any
) => {
  return apiSearchGetFetcher(
    url,
    token,
    new URLSearchParams({
      page: page.toString(),
      limit: itemsPerPage.toString(),
    })
  );
};

export const apiMethodFetcher = (
  url: string,
  token: string,
  method: string,
  body: any,
  asJson: boolean = true,
  ...props: any
) => {
  return fetch(apiRoute(url), {
    method,
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
    body: JSON.stringify(body),
  }).then((res) => (asJson ? res.json() : res));
};

export const getApiRouteCacheKey = (url: string, session: AppSession) => {
  return !session ? null : [url, session.accessToken];
};

export const getPagedApiRouteCacheKey = (
  url: string,
  session: AppSession,
  page: number,
  itemsPerPage: number
) => {
  return !session ? null : [url, session.accessToken, page, itemsPerPage];
};

export const getSearchApiRouteCacheKey = (
  url: string,
  session: AppSession,
  searchParams: URLSearchParams
) => {
  return !session
    ? null
    : [getSearchParamPath(url, searchParams), session.accessToken];
};

export const getPagedSearchApiRouteCacheKey = (
  url: string,
  session: AppSession,
  params: Record<string, string> & { page: string; limit: string }
) => {
  return getSearchApiRouteCacheKey(url, session, new URLSearchParams(params));
};

export const getApiSWRInfiniteKey = ({
  url,
  session,
  pageSize,
}: {
  url: string;
  session: AppSession;
  pageSize: number;
}) => {
  return (
    pageIndex: number,
    previousPageData: { totalPages: number } | null
  ) => {
    const pagedCacheKey = getPagedSearchApiRouteCacheKey(url, session, {
      page: (pageIndex + 1).toString(),
      limit: pageSize.toString(),
    });

    if (previousPageData)
      if (pageIndex < (previousPageData.totalPages ?? 0)) {
        return pagedCacheKey;
      } else {
        return null;
      }

    return pagedCacheKey;
  };
};

export const processSWRInfiniteData = (
  size: number,
  pageSize: number,
  isValidating: boolean,
  error: any,
  pagesDtos: { data?: any[] | undefined }[] | undefined
) => {
  const pages = pagesDtos ? [...pagesDtos] : [];
  const isLoadingInitialData = !pagesDtos && !error;
  const isLoadingMore =
    isLoadingInitialData ||
    (size > 0 && pagesDtos && typeof pagesDtos[size - 1] === "undefined");
  const isEmpty = pagesDtos?.[0]?.data?.length === 0;
  const isReachingEnd =
    isEmpty ||
    (pagesDtos &&
      (pagesDtos[pagesDtos.length - 1]?.data?.length ?? 0) < pageSize);
  const isRefreshing = isValidating && pagesDtos && pagesDtos.length == size;

  return {
    pages,
    isLoadingInitialData,
    isLoadingMore,
    isEmpty,
    isReachingEnd,
    isRefreshing,
  };
};

export const apiAccountsMe = "/api/Accounts/Me";
export const apiStudyYears = "/api/StudyYears";
export const apiFollowedStudyYears = "/api/FollowedStudyYears";
export const apiCourses = "/api/Courses";
export const apiFollowedCourses = "/api/FollowedCourses";
export const apiNotificationsMe = "/api/Notifications/Me";
export const apiStudyYear = (id: string) => `/api/StudyYears/${id}`;
export const apiCourse = (id: string) => `/api/Courses/${id}`;
export const apiCourseMaterialUploadRequests =
  "/api/CourseMaterialUploadRequests";
export const apiCourseMaterialUploadRequestCourse = (id: string) =>
  `/api/CourseMaterialUploadRequests/course/${id}`;
export const apiNotificationsMeUnread = "/api/Notifications/Me/Unread";
export const apiNotificationReadStatus = (id: string) =>
  `/api/Notifications/${id}/ReadStatus`;
export const apiStudyMaterials = "/api/StudyMaterials";
export const apiStudyMaterialsCourse = (id: string) =>
  `/api/StudyMaterials/course/${id}`;

export const getSearchParamPath = (
  url: string,
  searchParams: URLSearchParams
) => `${url}?` + searchParams;
