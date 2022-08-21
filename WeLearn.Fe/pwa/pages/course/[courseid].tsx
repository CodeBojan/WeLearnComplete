import { AppPageWithLayout } from "../_app";
import { GetCourseDto } from "../../types/api";
import { MdGrading } from "react-icons/md";
import TestButton from "../../components/atoms/testbutton";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import router from "next/router";
import { useState } from "react";

const Course: AppPageWithLayout = () => {
  const [course, setCourse] = useState<GetCourseDto>();
  const { courseId } = router.query;

  return (
    <TitledPageContainer icon={<MdGrading />} title={course?.code}>
      <>
        <TestButton variant="main"></TestButton>
      </>
    </TitledPageContainer>
  );
};

Course.getLayout = defaultGetLayout;

export default Course;
