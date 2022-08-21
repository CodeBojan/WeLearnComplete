import { GetCourseDto, GetCourseDtoPagedResponseDto } from "../../types/api";
import {
  apiCourses,
  apiGetFetcher,
  getPagedSearchApiRouteCacheKey,
} from "../../util/api";
import { queryTypes, useQueryState } from "next-usequerystate";
import { useEffect, useState } from "react";
import useSWR, { mutate } from "swr";

import { AiFillHeart } from "react-icons/ai";
import { AppPageWithLayout } from "../_app";
import Button from "../../components/atoms/button";
import CoursesList from "../../components/containers/courses-list";
import { MdSubject } from "react-icons/md";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { useAppSession } from "../../util/auth";

const Courses: AppPageWithLayout = () => {
  const { data: session, status } = useAppSession();
  const [courses, setCourses] = useState<GetCourseDto[]>([]); // TODO skeletonize courses by making nullable
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [page, setPage] = useState(1);
  const [onlyMine, setOnlyMine] = useState(false);
  const [mineQueryParam, setMineQueryParam] = useQueryState(
    "mine",
    queryTypes.boolean
  );
  
  // TODO useSWRInfinite

  const cacheKey = getPagedSearchApiRouteCacheKey(apiCourses, session, {
    page: page.toString(),
    limit: itemsPerPage.toString(),
    isFollowing: onlyMine.toString(),
  });

  const { data: pagedCourses, error } = useSWR<GetCourseDtoPagedResponseDto>(
    cacheKey,
    apiGetFetcher
  );

  useEffect(() => {
    if (mineQueryParam === null) {
      onlyMine && setMineQueryParam(false);
      return;
    }

    if (mineQueryParam != onlyMine) setOnlyMine(mineQueryParam);
  }, [mineQueryParam]);

  useEffect(() => {
    if (!pagedCourses || !pagedCourses.data) return;
    setCourses(pagedCourses.data);
  }, [pagedCourses]);

  return (
    <TitledPageContainer
      icon={!onlyMine ? <MdSubject /> : <AiFillHeart />}
      title={!onlyMine ? "Courses" : "My Courses"}
    >
      <div className="my-8">
        <Button
          variant={onlyMine ? "normal" : "outline"}
          padding="large"
          onClick={() => {
            setMineQueryParam(!mineQueryParam);
          }}
        >
          Only Mine
        </Button>
      </div>
      <CoursesList courses={courses} onMutate={() => mutate(cacheKey)} />
    </TitledPageContainer>
  );
};

Courses.getLayout = defaultGetLayout;

export default Courses;
