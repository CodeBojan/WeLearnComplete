import { GetCourseDto } from "../types/api";

export enum CoursesActionKind {
  SET_COURSES = "SET_COURSES",
}

export type CoursesAction = {
  type: CoursesActionKind.SET_COURSES;
  payload: GetCourseDto[];
};

export type CoursesState = {
  courses?: GetCourseDto[];
};

export const initialCoursesState = {
  courses: [],
};

export function coursesReducer(
  _state: CoursesState,
  action: CoursesAction
): CoursesState {
  switch (action.type) {
    case CoursesActionKind.SET_COURSES:
      return { ..._state, courses: action.payload };
    default:
      return _state;
  }
}
