import { CreateCourseAction, CreateCourseActionKind, CreateCourseState } from "../../store/create-course-store";

import Button from "../atoms/button";
import { Dispatch } from "react";
import Input from "../atoms/input";
import Modal from "./modal";

export function CreateCourseModal({
    createCourseState,
    createCourseDispatch,
    createCourse,
  }: {
    createCourseState: CreateCourseState;
    createCourseDispatch: Dispatch<CreateCourseAction>;
    createCourse: () => void;
  }) {
    return (
      <Modal
        body={
          <div className="flex flex-col gap-y-4 p-4">
            <Input
              label="Short Name"
              text={createCourseState.shortName}
              placeholder="C1"
              onChange={(e) =>
                createCourseDispatch({
                  type: CreateCourseActionKind.SET_SHORT_NAME,
                  payload: e.target.value,
                })
              }
            />
            <Input
              label="Full Name"
              text={createCourseState.fullName}
              placeholder="Course 1"
              onChange={(e) =>
                createCourseDispatch({
                  type: CreateCourseActionKind.SET_FULL_NAME,
                  payload: e.target.value,
                })
              }
            />
            <Input
              label="Course Code"
              text={createCourseState.code}
              placeholder="C123"
              onChange={(e) =>
                createCourseDispatch({
                  type: CreateCourseActionKind.SET_CODE,
                  payload: e.target.value,
                })
              }
            />
            <Input
              label="Description"
              text={createCourseState.description}
              placeholder="Course 1 teaches about ..."
              onChange={(e) =>
                createCourseDispatch({
                  type: CreateCourseActionKind.SET_DESCRIPTION,
                  payload: e.target.value,
                })
              }
            />
            <Input
              label="Staff"
              text={createCourseState.staff}
              placeholder="Professor 1, Professor 2"
              onChange={(e) =>
                createCourseDispatch({
                  type: CreateCourseActionKind.SET_STAFF,
                  payload: e.target.value,
                })
              }
            />
            <Input
              label="Rules"
              text={createCourseState.rules}
              placeholder="Students are required to ..."
              onChange={(e) =>
                createCourseDispatch({
                  type: CreateCourseActionKind.SET_RULES,
                  payload: e.target.value,
                })
              }
            />
          </div>
        }
        header={"Create a Course"}
        open={createCourseState.isOpen}
        footer={
          <div className="flex flex-row gap-x-2">
            <Button onClick={() => createCourse()}>Save</Button>
            <Button
              variant="danger"
              onClick={() => {
                createCourseDispatch({ type: CreateCourseActionKind.CLEAR });
              }}
            >
              Cancel
            </Button>
          </div>
        }
      />
    );
  }
  