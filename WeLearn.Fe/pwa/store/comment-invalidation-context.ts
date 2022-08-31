import { createContext } from "react";

export const CommentsInvalidationContext = createContext({
  commentsInvalidate: () => {},
});
