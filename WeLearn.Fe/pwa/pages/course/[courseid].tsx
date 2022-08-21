import { AiFillPlusSquare, AiFillPushpin, AiOutlinePlus } from "react-icons/ai";
import { apiCourse, apiGetFetcher, getApiRouteCacheKey } from "../../util/api";
import { useEffect, useState } from "react";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import Button from "../../components/atoms/button";
import CourseFollowInfo from "../../components/molecules/course-follow-info";
import { GetCourseDto } from "../../types/api";
import { ImPlus } from "react-icons/im";
import { MdGrading } from "react-icons/md";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import router from "next/router";
import { useAppSession } from "../../util/auth";

const Course: AppPageWithLayout = () => {
  const { courseId: courseId } = router.query as { courseId: string };
  const { data: session } = useAppSession();
  const [course, setCourse] = useState<GetCourseDto | null>(null);

  const cacheKey = getApiRouteCacheKey(apiCourse(courseId), session);

  const { data: courseResponse, error } = useSWR<GetCourseDto>(
    cacheKey,
    apiGetFetcher
  );

  useEffect(() => {
    if (!courseResponse) return;
    setCourse(courseResponse);
  }, [courseResponse]);

  return (
    <TitledPageContainer
      icon={<MdGrading />}
      title={
        course
          ? `[${course.code} - ${course.shortName}] ${course.fullName}`
          : null
      }
    >
      <div className="mt-8 w-full flex flex-row gap-x-4">
        <Button>
          {/* TODO add toggle on whole button */}
          <CourseFollowInfo
            isFollowing={course?.isFollowing}
            followingCount={course?.followingCount}
            courseId={course?.id}
            courseShortName={course?.shortName}
            onMutate={() => mutate(cacheKey)}
          />
        </Button>
        <Button
          onClick={() => {
            // TODO open modal to create upload material request
          }}
          className="flex flex-row gap-x-2 items-center"
          variant="outline"
        >
          <ImPlus className="text-lg" /> Upload Material
        </Button>
      </div>
      <div className="mt-8 flex flex-col w-full gap-y-8">
        {/* TODO accordion */}
        {course?.description && (
          <div className="flex flex-col">
            <span className="text-2xl font-bold">Course Description</span>
            <span className="text-lg">{course?.description}</span>
          </div>
        )}
        {course?.rules && (
          <div className="flex flex-col">
            <span className="text-2xl font-bold">Rules</span>
            <span>{course.rules}</span>
          </div>
        )}
        <div className="flex flex-col">
          <div className="text-2xl font-bold">Posts and Notices</div>
          <div className="">
            ...
            {/* TODO filter load only posts and notices */}
          </div>
        </div>
        <div className="flex flex-col">
          <div className="text-2xl font-bold">Course Material</div>
          <div className="">...</div>
        </div>
      </div>
    </TitledPageContainer>
  );
};

Course.getLayout = defaultGetLayout;

export default Course;
