import { WeLearnProblemDetails } from "../types/isApi";

export const isApiBaseUrl = process.env.NEXT_PUBLIC_IS_API;

export function isApiRoute(route: string) {
  return `${isApiBaseUrl}${route}`;
}

export const isApiMethodFetcher = (
  url: string,
  token: string,
  method: string,
  body?: any | undefined,
  asJson: boolean = true,
  ...props: any
) => {
  return fetch(isApiRoute(url), {
    method,
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
    body: JSON.stringify(body),
  }).then((res) => {
    if (!res.ok) {
      console.error("not ok ", res.status);
      throw res.json() as Promise<WeLearnProblemDetails>;
    }

    return asJson ? res.json() : res;
  });
};

export const isApiStudyYearAccountRoles = "/api/StudyYearAccountRoles";
export const isApiCourseAccountRoles = "/api/CourseAccountRoles";
