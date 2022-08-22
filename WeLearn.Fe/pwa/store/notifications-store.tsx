import { createContext } from "react";

export enum NotificationsActionKind {
  SET_UNREAD_NOTIFICATION_COUNT = "SET_UNREAD_NOTIFICATION_COUNT",
}

export type NotificationsAction = {
  type: NotificationsActionKind.SET_UNREAD_NOTIFICATION_COUNT;
  unreadCount: number;
};

export type NotificationsState = {
  unreadCount: number;
};

export const initialNotificationsState: NotificationsState = {
  unreadCount: 0,
};

export function notificationsReducer(
  _state: NotificationsState,
  action: NotificationsAction
): NotificationsState {
  switch (action.type) {
    case NotificationsActionKind.SET_UNREAD_NOTIFICATION_COUNT:
      console.log("setting unread count to " + action.unreadCount);
      return { ..._state, unreadCount: action.unreadCount };
    default:
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
  }
}

export const NotificationsContext = createContext({
  state: initialNotificationsState,
  dispatch: (action: NotificationsAction) => {},
});

export const NotificationsInvalidationContext = createContext({
  notificationsInvalidate: () => {},
});
