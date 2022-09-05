import { queryTypes, useQueryState } from "next-usequerystate";
import { useEffect, useState } from "react";

import { AiFillHeart } from "react-icons/ai";
import { AppPageWithLayout } from "../_app";
import CoursesList from "../../components/containers/courses-list";
import { MdSubject } from "react-icons/md";
import OnlyMineButton from "../../components/atoms/only-mine-button";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { useAppSession } from "../../util/auth";
import useCourses from "../../util/useCourses";

const Courses: AppPageWithLayout = () => {
  const { data: session, status } = useAppSession();
  const [onlyMine, setOnlyMine] = useState(false);
  const [mineQueryParam, setMineQueryParam] = useQueryState(
    "mine",
    queryTypes.boolean
  );
  const { courses, size, setSize, isLoadingMore, isReachingEnd, mutate } =
    useCourses({ followingOnly: onlyMine });

  useEffect(() => {
    if (mineQueryParam === null) {
      onlyMine && setMineQueryParam(false);
      return;
    }

    if (mineQueryParam != onlyMine) setOnlyMine(mineQueryParam);
  }, [mineQueryParam]);

  return (
    <TitledPageContainer
      icon={!onlyMine ? <MdSubject /> : <AiFillHeart />}
      title={!onlyMine ? "Courses" : "My Courses"}
    >
      <div className="my-4">
        <OnlyMineButton
          onlyMine={onlyMine}
          onClick={() => setMineQueryParam(!mineQueryParam)}
        />
      </div>
      {courses ? (
        <CoursesList courses={courses} onMutate={() => mutate()} />
      ) : (
        <div>Loading...</div>
      )}
    </TitledPageContainer>
  );
};

Courses.getLayout = defaultGetLayout;

export default Courses;
