import {
  GetCourseMaterialUploadRequestDto,
  GetCourseMaterialUploadRequestDtoPagedResponseDto,
} from "../types/api";

export enum UnapprovedCourseMaterialUploadRequestActionKind {
  SET_REQUESTS = "SET_REQUESTS", // TODO remove SET_REQUESTS // TODO rename store and the component that uses it to modal
  OPEN_MODAL = "OPEN_MODAL",
  CLOSE_MODAL = "CLOSE_MODAL",
}

export type UnapprovedCourseMaterialUploadRequestAction =
  | {
      type: UnapprovedCourseMaterialUploadRequestActionKind.SET_REQUESTS;
      payload: GetCourseMaterialUploadRequestDto[];
    }
  | {
      type: UnapprovedCourseMaterialUploadRequestActionKind.OPEN_MODAL;
    }
  | {
      type: UnapprovedCourseMaterialUploadRequestActionKind.CLOSE_MODAL;
    };

export type UnapprovedCourseMaterialUploadRequestState = {
  requests?: GetCourseMaterialUploadRequestDto[];
  modalOpen: boolean;
};

export const initialUnapprovedCourseMaterialUploadRequestState: UnapprovedCourseMaterialUploadRequestState =
  {
    requests: [],
    modalOpen: false,
  };

export function unapprovedCourseMaterialUploadRequestReducer(
  _state: UnapprovedCourseMaterialUploadRequestState,
  action: UnapprovedCourseMaterialUploadRequestAction
): UnapprovedCourseMaterialUploadRequestState {
  switch (action.type) {
    case UnapprovedCourseMaterialUploadRequestActionKind.SET_REQUESTS:
      return { ..._state, requests: action.payload };
    case UnapprovedCourseMaterialUploadRequestActionKind.OPEN_MODAL:
      return { ..._state, modalOpen: true };
    case UnapprovedCourseMaterialUploadRequestActionKind.CLOSE_MODAL:
      return { ..._state, modalOpen: false };
    default: {
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
    }
  }
}
