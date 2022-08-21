import { AppPageWithLayout } from "../_app";
import { GetCourseDto } from "../../types/api";
import { MdGrading } from "react-icons/md";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import router from "next/router";
import { useState } from "react";

const Course: AppPageWithLayout = () => {
  const [course, setCourse] = useState<GetCourseDto>();

  return (
    <TitledPageContainer icon={<MdGrading />} title={course?.code}>
      <></>
    </TitledPageContainer>
  );
};

Course.getLayout = defaultGetLayout;

export default Course;
