import { GetAccountDto } from "../types/api";

export enum AccountSelectorActionKind {
  OPEN_MODAL = "OPEN_MODAL",
  CLOSE_MODAL = "CLOSE_MODAL",
  SELECT_ACCOUNT = "SELECT_ACCOUNT",
  CLEAR_MODAL = "CLEAR_MODAL",
}

export type AccountSelectorAction =
  | {
      type: AccountSelectorActionKind.OPEN_MODAL;
    }
  | { type: AccountSelectorActionKind.CLOSE_MODAL }
  | { type: AccountSelectorActionKind.SELECT_ACCOUNT; payload: GetAccountDto }
  | { type: AccountSelectorActionKind.CLEAR_MODAL };

export type AccountSelectorState = {
  isOpen: boolean;
  selectedUser: GetAccountDto | null;
};

export const initialAccountSelectorState: AccountSelectorState = {
  isOpen: false,
  selectedUser: null,
};

export function accountSelectorReducer(
  _state: AccountSelectorState,
  action: AccountSelectorAction
): AccountSelectorState {
  switch (action.type) {
    case AccountSelectorActionKind.OPEN_MODAL:
      return {
        ..._state,
        isOpen: true,
      };
    case AccountSelectorActionKind.CLOSE_MODAL:
      return {
        ..._state,
        isOpen: false,
      };
    case AccountSelectorActionKind.SELECT_ACCOUNT:
      return {
        ..._state,
        selectedUser: action.payload,
      };
    case AccountSelectorActionKind.CLEAR_MODAL:
      return {
        ..._state,
        selectedUser: null,
      };
    default: {
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
    }
  }
}
