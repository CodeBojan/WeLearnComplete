export enum CreateCourseActionKind {
  OPEN_MODAL = "OPEN_MODAL",
  CLOSE_MODAL = "CLOSE_MODAL",
  SET_SHORT_NAME = "SET_SHORT_NAME",
  SET_FULL_NAME = "SET_FULL_NAME",
  SET_DESCRIPTION = "SET_DESCRIPTION",
  SET_STAFF = "SET_STAFF",
  SET_RULES = "SET_RULES",
  SET_CODE = "SET_CODE",
  CLEAR = "CLEAR",
}

export type CreateCourseAction =
  | { type: CreateCourseActionKind.OPEN_MODAL }
  | { type: CreateCourseActionKind.CLOSE_MODAL }
  | { type: CreateCourseActionKind.SET_SHORT_NAME; payload: string }
  | { type: CreateCourseActionKind.SET_FULL_NAME; payload: string }
  | { type: CreateCourseActionKind.SET_DESCRIPTION; payload: string }
  | { type: CreateCourseActionKind.SET_STAFF; payload: string }
  | { type: CreateCourseActionKind.SET_RULES; payload: string }
  | { type: CreateCourseActionKind.SET_CODE; payload: string }
  | { type: CreateCourseActionKind.CLEAR };

export type CreateCourseState = {
  isOpen: boolean;
  shortName: string;
  fullName: string;
  description: string;
  staff: string;
  rules: string;
  code: string;
};

export const initialCreateCourseState: CreateCourseState = {
  isOpen: false,
  shortName: "",
  fullName: "",
  description: "",
  staff: "",
  rules: "",
  code: "",
};

export function createCourseReducer(
  _state: CreateCourseState,
  action: CreateCourseAction
): CreateCourseState {
  switch (action.type) {
    case CreateCourseActionKind.OPEN_MODAL:
      return { ..._state, isOpen: true };
    case CreateCourseActionKind.CLOSE_MODAL:
      return { ..._state, isOpen: false };
    case CreateCourseActionKind.SET_SHORT_NAME:
      return { ..._state, shortName: action.payload };
    case CreateCourseActionKind.SET_FULL_NAME:
      return { ..._state, fullName: action.payload };
    case CreateCourseActionKind.SET_DESCRIPTION:
      return { ..._state, description: action.payload };
    case CreateCourseActionKind.SET_STAFF:
      return { ..._state, staff: action.payload };
    case CreateCourseActionKind.SET_RULES:
      return { ..._state, rules: action.payload };
    case CreateCourseActionKind.SET_CODE:
      return { ..._state, code: action.payload };
    case CreateCourseActionKind.CLEAR:
      return { ...initialCreateCourseState };
    default: {
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
    }
  }
}
