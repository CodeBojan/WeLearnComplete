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
  method: "GET" | "POST" | "PUT" | "DELETE",
  body?: any | undefined,
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
  url: string | null,
  session: AppSession,
  page: number,
  itemsPerPage: number
) => {
  return !session ? null : [url, session.accessToken, page, itemsPerPage];
};

export const getSearchApiRouteCacheKey = (
  url: string | null,
  session: AppSession,
  searchParams: URLSearchParams
) => {
  return !session
    ? null
    : !url
    ? null
    : [getSearchParamPath(url, searchParams), session.accessToken];
};

export const getPagedSearchApiRouteCacheKey = (
  url: string | null,
  session: AppSession,
  params: Record<string, string> & { page: string; limit: string }
) => {
  return getSearchApiRouteCacheKey(url, session, new URLSearchParams(params));
};

export const getApiSWRInfiniteKey = ({
  url,
  session,
  pageSize,
  queryParams,
}: {
  url: string | null;
  session: AppSession;
  pageSize: number;
  queryParams?: Record<string, string>;
}) => {
  return (
    pageIndex: number,
    previousPageData: { totalPages: number } | null
  ) => {
    console.log(queryParams);
    const pagedCacheKey = getPagedSearchApiRouteCacheKey(url, session, {
      ...queryParams,
      page: (pageIndex + 1).toString(),
      limit: pageSize.toString(),
    });

    console.log(pagedCacheKey);

    if (previousPageData)
      if (pageIndex < (previousPageData.totalPages ?? 0)) {
        return pagedCacheKey;
      } else {
        return null;
      }

    return pagedCacheKey;
  };
};

export type Entity = {
  id?: string | undefined;
};

export type PageDtos<TEntity extends Entity> = {
  data?: TEntity[] | undefined;
  page?: number | undefined;
  totalPages?: number | undefined;
};

export const processSWRInfiniteData = <
  TEntity extends Entity,
  TPagedEntity extends PageDtos<TEntity> = PageDtos<TEntity>
>(
  size: number,
  pageSize: number,
  isValidating: boolean,
  error: any,
  pagesDtos: TPagedEntity[] | undefined
) => {
  const pages = pagesDtos ? [...pagesDtos] : [];
  const isLoadingInitialData = !pagesDtos && !error;
  const isLoadingMore =
    isLoadingInitialData ||
    (size > 0 && pagesDtos && typeof pagesDtos[size - 1] === undefined);
  const isEmpty = pagesDtos?.[0]?.data?.length === 0;
  const isReachingEnd =
    isEmpty ||
    (pagesDtos &&
      (pagesDtos[pagesDtos.length - 1]?.page ?? 0) ===
        (pagesDtos[pagesDtos.length - 1]?.totalPages ?? 0));
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
export const apiDocuments = "/api/Documents";
export const apiDocument = (id: string) => `/api/Documents/${id}`;
export const apiStudyYearAccounts = (id: string) =>
  `/api/StudyYears/${id}/Accounts`;
export const apiStudyYearNotices = (id: string) =>
  `/api/Notices/StudyYear/${id}`;
export const apiCourseAccounts = (id: string) => `/api/Courses/${id}/Accounts`;
export const apiUnapprovedStudyMaterialsCourse = (id: string) =>
  `/api/CourseMaterialUploadRequests/Course/${id}/Unapproved`;
export const apiContentComments = (id: string) => `/api/Comments/content/${id}`;
export const apiCourseMaterialUploadRequestApprovers = (id: string) =>
  `/api/CourseMaterialUploadRequests/${id}/approve`;
export const apiCourseContent = (id: string) => `/api/Content/Course/${id}`;
export const apiFeed = `/api/Feed`;

export const getSearchParamPath = (
  url: string | null,
  searchParams: URLSearchParams
) => (!url ? null : `${url}?` + searchParams);
