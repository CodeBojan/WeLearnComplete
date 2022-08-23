import {
  GetCourseDto,
  GetCourseDtoPagedResponseDto,
  GetStudyYearDto,
} from "../../types/api";
import {
  apiCourses,
  apiGetFetcher,
  apiStudyYear,
  getApiRouteCacheKey,
  getPagedSearchApiRouteCacheKey,
} from "../../util/api";
import { queryTypes, useQueryState } from "next-usequerystate";
import router, { useRouter } from "next/router";
import { useEffect, useState } from "react";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import CoursesList from "../../components/containers/courses-list";
import { FaRegCalendarAlt } from "react-icons/fa";
import { MdCalendarViewMonth } from "react-icons/md";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { useAppSession } from "../../util/auth";

const StudyYear: AppPageWithLayout = () => {
  const { studyYearId } = router.query as { studyYearId: string };
  const { data: session } = useAppSession();
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

  // TODO
  // TODO get study year courses from API
  return (
    <TitledPageContainer
      icon={<FaRegCalendarAlt />}
      title={
        studyYear ? `[${studyYear.shortName}] ${studyYear.fullName}` : null
      }
    >
      <div className="my-8">{studyYear?.description}</div>
      <div className="w-full mb-4 flex flex-col gap-y-4">
        <div className="text-2xl font-bold">Courses</div>
        <div>
          <CoursesList courses={courses} onMutate={() => mutate(cacheKey)} />
        </div>
      </div>
    </TitledPageContainer>
  );
};

StudyYear.getLayout = defaultGetLayout;

export default StudyYear;
