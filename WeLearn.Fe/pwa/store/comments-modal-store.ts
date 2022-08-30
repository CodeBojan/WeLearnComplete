import { createContext } from "react";

export enum CommentsModalActionKind {
  OPEN_MODAL = "OPEN_MODAL",
  CLOSE_MODAL = "CLOSE_MODAL",
  SET_CONTENT_ID = "SET_CONTENT_ID",
  SET_COMMENT_TEXT = "SET_COMMENT_TEXT",
  INVALIDATE_COMMENTS = "INVALIDATE_COMMENTS",
  CLEAR = "CLEAR",
}

export type CommentsModalAction =
  | {
      type: CommentsModalActionKind.OPEN_MODAL;
    }
  | {
      type: CommentsModalActionKind.CLOSE_MODAL;
    }
  | {
      type: CommentsModalActionKind.SET_CONTENT_ID;
      payload: string;
    }
  | {
      type: CommentsModalActionKind.CLEAR;
    }
  | {
      type: CommentsModalActionKind.INVALIDATE_COMMENTS;
    }
  | {
      type: CommentsModalActionKind.SET_COMMENT_TEXT;
      payload: string;
    };

export type CommentsModalState = {
  contentId: string | null;
  isOpen: boolean;
  commentText: string;
};

export const initialCommentsModalState: CommentsModalState = {
  contentId: null,
  isOpen: false,
  commentText: "",
};

export function commentsModalReducer(
  _state: CommentsModalState,
  action: CommentsModalAction
): CommentsModalState {
  switch (action.type) {
    case CommentsModalActionKind.OPEN_MODAL:
      // TODO invalidate?
      return {
        ..._state,
        isOpen: true,
      };
    case CommentsModalActionKind.CLOSE_MODAL:
      return {
        ..._state,
        isOpen: false,
      };
    case CommentsModalActionKind.SET_CONTENT_ID:
      // TODO invalidate?
      return {
        ..._state,
        contentId: action.payload,
      };
    case CommentsModalActionKind.CLEAR:
      return {
        ..._state,
        contentId: null,
      };
    case CommentsModalActionKind.INVALIDATE_COMMENTS:
      // TODO implement
      return {
        ..._state,
      };
    case CommentsModalActionKind.SET_COMMENT_TEXT:
      return {
        ..._state,
        commentText: action.payload,
      };

    default: {
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
    }
  }
}

export const CommentsModalContext = createContext({
  state: initialCommentsModalState,
  dispatch: (action: CommentsModalAction) => {},
});
