import { GetAccountDto } from "../types/api";
import { createContext } from "react";

export enum MeActionKind {
  SET_ME = "SET_ME",
  SET_SHOULD_UPDATE = "SET_SHOULD_UPDATE",
}

export type MeAction =
  | { type: MeActionKind.SET_ME; payload: GetAccountDto }
  | {
      type: MeActionKind.SET_SHOULD_UPDATE;
    };

export type MeState = {
  account?: GetAccountDto | null;
  updateRequested?: Date | null;
};

export const initialMeState: MeState = {
  account: null,
  updateRequested: null,
};

export function meReducer(_state: MeState, action: MeAction): MeState {
  switch (action.type) {
    case MeActionKind.SET_ME:
      return {
        ..._state,
        account: action.payload,
      };
    case MeActionKind.SET_SHOULD_UPDATE:
      const now = new Date();
      return { ..._state, updateRequested: now };
    default: {
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
    }
  }
}

export const MeContext = createContext({
  state: initialMeState,
  dispatch: (action: MeAction) => {},
});

export const MeInvalidationContext = createContext({
  meInvalidate: () => {},
});
