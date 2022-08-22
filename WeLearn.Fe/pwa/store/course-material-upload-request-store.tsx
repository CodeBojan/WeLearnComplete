export enum CourseMaterialUploadRequestActionKind {
  SET_BODY = "SET_BODY",
  SET_REMARK = "SET_REMARK",
  ADD_FILE = "ADD_FILE",
}

export type CourseMaterialUploadRequestAction =
  | { type: CourseMaterialUploadRequestActionKind.SET_BODY; body: string }
  | { type: CourseMaterialUploadRequestActionKind.SET_REMARK; remark: string }
  | { type: CourseMaterialUploadRequestActionKind.ADD_FILE; file: File };

export type CourseMaterialUploadRequestState = {
  body: string | null;
  remark: string | null;
  files: File[];
};

export const initialState: CourseMaterialUploadRequestState = {
  body: null,
  remark: null,
  files: [],
};

export function courseMaterialUploadRequestReducer(
  _state: CourseMaterialUploadRequestState,
  action: CourseMaterialUploadRequestAction
): CourseMaterialUploadRequestState {
  switch (action.type) {
    case CourseMaterialUploadRequestActionKind.SET_BODY:
      return { ..._state, body: action.body };
    case CourseMaterialUploadRequestActionKind.SET_REMARK:
      return { ..._state, remark: action.remark };
    case CourseMaterialUploadRequestActionKind.ADD_FILE:
      return { ..._state, files: [..._state.files, action.file] };
    default:
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
  }
}
