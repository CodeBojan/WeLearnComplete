import router, { useRouter } from "next/router";

import { AppPageWithLayout } from "../_app";
import { FaRegCalendarAlt } from "react-icons/fa";
import { GetStudyYearDto } from "../../types/api";
import { MdCalendarViewMonth } from "react-icons/md";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { useState } from "react";

const StudyYear: AppPageWithLayout = () => {
  const [studyYear, setStudyYear] = useState<GetStudyYearDto | null>(null);

  const { studyYearId } = router.query;

  // TODO get study year from API
  return (
    <TitledPageContainer icon={<FaRegCalendarAlt />} title={"Test"}>
      <>{studyYearId}</>
    </TitledPageContainer>
  );
};

StudyYear.getLayout = defaultGetLayout;

export default StudyYear;
