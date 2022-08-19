import { GetStudyYearDto } from "../types/api";

export enum StudyYearsActionKind {
  SET_STUDY_YEARS = "SET_STUDY_YEARS",
}

export type StudyYearsAction = {
  type: StudyYearsActionKind.SET_STUDY_YEARS;
  payload: GetStudyYearDto[];
};

export type StudyYearsState = {
  studyYears?: GetStudyYearDto[];
};

export const initialStudyYearsState: StudyYearsState = {
  studyYears: [],
};

export function studyYearsReducer(
  _state: StudyYearsState,
  action: StudyYearsAction
): StudyYearsState {
  switch (action.type) {
    case StudyYearsActionKind.SET_STUDY_YEARS:
      return { ..._state, studyYears: action.payload };
    default: {
      throw new Error(`Unhandled action type - ${JSON.stringify(action)}`);
    }
  }
}
