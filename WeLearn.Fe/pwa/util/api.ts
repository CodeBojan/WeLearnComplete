export const apiBaseUrl = process.env.NEXT_PUBLIC_API;

export function apiRoute(route: string) {
  return `${apiBaseUrl}${route}`;
}

export const apiGetFetcher = (url: string, token: string) =>
  fetch(apiRoute(url), {
    headers: {
      Authorization: "Bearer " + token,
    },
  }).then((res) => res.json());
