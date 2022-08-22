export enum CourseMaterialUploadRequestActionKind {
  SET_BODY = "SET_BODY",
  SET_REMARK = "SET_REMARK",
  ADD_FILE = "ADD_FILE",
  CLEAR_FILES = "CLEAR_FILES",
  REMOVE_FILE = "REMOVE_FILE",
  CLEAR = "CLEAR",
}

export type CourseMaterialUploadRequestAction =
  | { type: CourseMaterialUploadRequestActionKind.SET_BODY; body: string }
  | { type: CourseMaterialUploadRequestActionKind.SET_REMARK; remark: string }
  | { type: CourseMaterialUploadRequestActionKind.ADD_FILE; file: File }
  | { type: CourseMaterialUploadRequestActionKind.CLEAR_FILES }
  | { type: CourseMaterialUploadRequestActionKind.REMOVE_FILE; file: File }
  | { type: CourseMaterialUploadRequestActionKind.CLEAR };

export type CourseMaterialUploadRequestState = {
  body: string | null;
  remark: string | null;
  files: File[];
};

export const initialCourseMaterialUploadRequestState: CourseMaterialUploadRequestState =
  {
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
      if (containsFile(_state.files, action.file)) return _state;

      return {
        ..._state,
        files: [..._state.files, action.file],
      };
    case CourseMaterialUploadRequestActionKind.CLEAR_FILES:
      return { ..._state, files: [] };
    case CourseMaterialUploadRequestActionKind.REMOVE_FILE:
      return {
        ..._state,
        files: _state.files.filter((f) => f !== action.file),
      };
    case CourseMaterialUploadRequestActionKind.CLEAR:
      return initialCourseMaterialUploadRequestState;
    default:
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
  }
}
export function containsFile(files: File[], file: File) {
  return files.find(
    (f) =>
      f.name === file.name &&
      f.size === file.size &&
      f.type === file.type &&
      f.lastModified === file.lastModified
  );
}
