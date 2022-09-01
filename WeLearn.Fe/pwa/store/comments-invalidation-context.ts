import { createContext } from "react";

export enum CommentsInvalidationActionKind {
  INVALIDATE = "INVALIDATE",
}

export type CommentsInvalidationAction = {
  type: CommentsInvalidationActionKind.INVALIDATE;
};

export type CommentsInvalidationState = {
  lastInvalidated: Date;
};

export const initialCommentsInvalidationState: CommentsInvalidationState = {
  lastInvalidated: new Date(),
};

export function commentsInvalidationReducer(
  _state: CommentsInvalidationState,
  action: CommentsInvalidationAction
): CommentsInvalidationState {
  switch (action.type) {
    case CommentsInvalidationActionKind.INVALIDATE:
      return {
        ..._state,
        lastInvalidated: new Date(),
      };
    default: {
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
    }
  }
}

export const CommentsInvalidationContext = createContext({
  commentsInvalidationState: initialCommentsInvalidationState,
  commentsInvalidate: () => {},
});
