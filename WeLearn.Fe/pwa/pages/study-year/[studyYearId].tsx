import {
  CreateCourseAction,
  CreateCourseActionKind,
  CreateCourseState,
  createCourseReducer,
  initialCreateCourseState,
} from "../../store/create-course-store";
import { Dispatch, useEffect, useReducer, useState } from "react";
import {
  GetCourseDto,
  GetCourseDtoPagedResponseDto,
  GetStudyYearDto,
  PostCourseDto,
} from "../../types/api";
import {
  apiCourses,
  apiGetFetcher,
  apiMethodFetcher,
  apiStudyYear,
  getApiRouteCacheKey,
  getPagedSearchApiRouteCacheKey,
} from "../../util/api";
import { isStudyYearAdmin, useAppSession } from "../../util/auth";
import { queryTypes, useQueryState } from "next-usequerystate";
import router, { useRouter } from "next/router";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import Button from "../../components/atoms/button";
import CoursesList from "../../components/containers/courses-list";
import { CreateCourseModal } from "../../components/molecules/create-course-modal";
import { FaRegCalendarAlt } from "react-icons/fa";
import { GrUserAdmin } from "react-icons/gr";
import Input from "../../components/atoms/input";
import { MdCalendarViewMonth } from "react-icons/md";
import Modal from "../../components/molecules/modal";
import OnlyMineButton from "../../components/atoms/only-mine-button";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { toast } from "react-toastify";

const StudyYear: AppPageWithLayout = () => {
  const { studyYearId } = router.query as { studyYearId: string };
  const { data: session } = useAppSession();
  const isAdmin = isStudyYearAdmin(session.user, studyYearId);
  const [studyYear, setStudyYear] = useState<GetStudyYearDto | null>(null);

  const cacheKey = getApiRouteCacheKey(apiStudyYear(studyYearId), session);

  const { data: studyYearResponse, error } = useSWR<GetStudyYearDto>(
    cacheKey,
    apiGetFetcher
  );

  // TODO refactor into its own hook/reducer
  const [courses, setCourses] = useState<GetCourseDto[]>([]);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [page, setPage] = useState(1);
  const [onlyMine, setOnlyMine] = useState(false);
  const [mineQueryParam, setMineQueryParam] = useQueryState(
    "mine",
    queryTypes.boolean
  );
  const courseCacheKey = getPagedSearchApiRouteCacheKey(apiCourses, session, {
    page: page.toString(),
    limit: itemsPerPage.toString(),
    isFollowing: onlyMine.toString(),
    studyYearId: studyYearId,
  });

  const [createCourseState, createCourseDispatch] = useReducer(
    createCourseReducer,
    initialCreateCourseState
  );

  const { data: pagedCourses, error: coursesError } =
    useSWR<GetCourseDtoPagedResponseDto>(courseCacheKey, apiGetFetcher);

  useEffect(() => {
    if (!studyYearResponse) return;
    setStudyYear(studyYearResponse);
  }, [studyYearResponse]);

  useEffect(() => {
    if (!pagedCourses || !pagedCourses.data) return;
    setCourses(pagedCourses.data);
  }, [pagedCourses]);

  function clearCreateCourseModal() {
    createCourseDispatch({
      type: CreateCourseActionKind.CLEAR,
    });
  }

  function createCourse(): void {
    apiMethodFetcher(apiCourses, session.accessToken, "POST", {
      code: createCourseState.code,
      description: createCourseState.description,
      fullName: createCourseState.fullName,
      shortName: createCourseState.shortName,
      studyYearId: studyYearId,
      rules: createCourseState.rules,
      staff: createCourseState.staff,
    } as PostCourseDto)
      .then((res) => {
        const courseDto = res as GetCourseDto;
        toast(`Course ${courseDto.shortName} created`, { type: "success" });
        mutate(courseCacheKey);
        clearCreateCourseModal();
      })
      .catch((err) => {
        toast(`Failed to create course: ${err.message}`, { type: "error" });
      });
  }

  return (
    <TitledPageContainer
      icon={<FaRegCalendarAlt />}
      title={
        studyYear ? (
          <div className="flex flex-row items-center justify-start gap-x-4">
            {`[${studyYear.shortName}] ${studyYear.fullName}`}
          </div>
        ) : null
      }
    >
      <div className="flex flex-col my-4">
        <div className="italic font-medium text-gray-400">
          updated at {studyYear?.updatedDate?.toString()}
        </div>
        <div>created at {studyYear?.createdDate?.toString()}</div>
      </div>
      <div className="flex flex-row gap-x-4 items-center">
        {isAdmin && (
          <>
            <GrUserAdmin className="text-xl" />
            <Button
              variant="outline"
              onClick={() =>
                createCourseDispatch({
                  type: CreateCourseActionKind.OPEN_MODAL,
                })
              }
            >
              Add Course
            </Button>
            <Button variant="outline">View Users</Button>
          </>
        )}
      </div>
      <div className="my-8">{studyYear?.description}</div>
      <div className="w-full mb-4 flex flex-col gap-y-4">
        <div className="text-2xl font-bold">Courses</div>
        <div className="">
          <div className="mb-8">
            <OnlyMineButton
              onlyMine={onlyMine}
              onClick={() => {
                setOnlyMine(!onlyMine);
                mutate(courseCacheKey);
              }}
            />
          </div>
          <CoursesList
            courses={courses}
            onMutate={() => mutate(courseCacheKey)}
          />
        </div>
      </div>
      <CreateCourseModal
        createCourseState={createCourseState}
        createCourseDispatch={createCourseDispatch}
        createCourse={createCourse}
      />
    </TitledPageContainer>
  );
};

StudyYear.getLayout = defaultGetLayout;

export default StudyYear;
