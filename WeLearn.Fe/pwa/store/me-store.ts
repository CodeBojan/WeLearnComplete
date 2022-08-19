import { GetAccountDto } from "../types/api";

export enum MeActionKind {
  SET_ME = "SET_ME",
}

export type MeAction = { type: MeActionKind.SET_ME; payload: GetAccountDto };

export type MeState = {
  account?: GetAccountDto | unknown;
};

export const initialMeState: MeState = {
  account: null,
};

export function meReducer(_state: MeState, action: MeAction): MeState {
  switch (action.type) {
    case MeActionKind.SET_ME:
      return { ..._state, account: action.payload };
    default: {
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
    }
  }
}
