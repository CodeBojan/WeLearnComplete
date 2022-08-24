import {
  CourseMaterialUploadRequestActionKind,
  courseMaterialUploadRequestReducer,
  initialCourseMaterialUploadRequestState,
} from "../../store/course-material-upload-request-store";
import {
  GetCourseDto,
  PostCourseMaterialUploadRequestDto,
  PostDocumentDto,
} from "../../types/api";
import {
  apiCourse,
  apiCourseMaterialUploadRequestCourse,
  apiGetFetcher,
  apiRoute,
  getApiRouteCacheKey,
} from "../../util/api";
import { useEffect, useReducer, useState } from "react";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import Button from "../../components/atoms/button";
import CourseFollowInfo from "../../components/molecules/course-follow-info";
import CourseMaterialUploadRequestModal from "../../components/molecules/course-material-upload-request-modal";
import CourseMaterials from "../../components/molecules/course-materials";
import { ImPlus } from "react-icons/im";
import { MdGrading } from "react-icons/md";
import { ReactNode } from "react";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import router from "next/router";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";

const Course: AppPageWithLayout = () => {
  const { courseId: courseId } = router.query as { courseId: string };
  const { data: session } = useAppSession();
  const [course, setCourse] = useState<GetCourseDto | null>(null);
  const [materialUploadState, materialUploadDispatch] = useReducer(
    courseMaterialUploadRequestReducer,
    initialCourseMaterialUploadRequestState
  );

  const cacheKey = getApiRouteCacheKey(apiCourse(courseId), session);

  const { data: courseResponse, error } = useSWR<GetCourseDto>(
    cacheKey,
    apiGetFetcher
  );

  useEffect(() => {
    if (!courseResponse) return;
    setCourse(courseResponse);
  }, [courseResponse]);

  const [uploadModalOpen, setUploadModalOpen] = useState(false);

  function clearModalState() {
    materialUploadDispatch({
      type: CourseMaterialUploadRequestActionKind.CLEAR,
    });
  }

  function addFile(file: File) {
    materialUploadDispatch({
      type: CourseMaterialUploadRequestActionKind.ADD_FILE,
      file,
    });
  }

  function removeFile(file: File) {
    materialUploadDispatch({
      type: CourseMaterialUploadRequestActionKind.REMOVE_FILE,
      file,
    });
  }

  function clearFiles() {
    materialUploadDispatch({
      type: CourseMaterialUploadRequestActionKind.CLEAR_FILES,
    });
  }

  function upload(): void {
    if (materialUploadState.files.length === 0) {
      toast.error("You must select at least one file");
      return;
    }
    if (!materialUploadState.body || materialUploadState.body.length === 0) {
      toast.error("You must enter a request body");
      return;
    }
    if (
      !materialUploadState.remark ||
      materialUploadState.remark.length === 0
    ) {
      toast.error("You must enter a request remark");
      return;
    }
    const formData = new FormData();
    // TODO extract to api function
    const dto = {
      body: materialUploadState.body,
      remark: materialUploadState.remark,
      courseId: courseId,
      documents: materialUploadState.files.map((f) => {
        return new PostDocumentDto({
          courseId: courseId,
        });
      }),
    } as PostCourseMaterialUploadRequestDto;
    formData.append("PostDto", JSON.stringify(dto));
    materialUploadState.files.forEach((file) => {
      formData.append("Files", file, file.name);
    });

    fetch(apiRoute(apiCourseMaterialUploadRequestCourse(courseId)), {
      method: "POST",
      headers: {
        Authorization: `Bearer ${session.accessToken}`,
      },
      body: formData,
    })
      .then((res) => {
        // TODO extract error throwing - use problem details ieee
        if (!res.ok) throw new Error(res.statusText);
        return res.json;
      })
      .then((res) => {
        setUploadModalOpen(false);
        clearModalState();
        mutate(cacheKey);
        toast.success("Upload successful");
      })
      .catch((error) => {
        toast(`Failed to create a request: ${JSON.stringify(error)}`, {
          type: "error",
        });
      });
  }

  const renderAccordionSection = ({
    title,
    content,
  }: {
    title: ReactNode | null;
    content: ReactNode | null;
  }) =>
    title &&
    content && (
      <div className="flex flex-col">
        <span className="text-2xl font-bold">{title}</span>
        <span className="text-lg">{content}</span>
      </div>
    );
  return (
    <TitledPageContainer
      icon={<MdGrading />}
      title={
        course
          ? `[${course.code} - ${course.shortName}] ${course.fullName}`
          : null
      }
    >
      <div className="flex flex-col items-start justify-start mt-4">
        <span className="italic font-medium text-gray-400">
          {/* TODO formatting */}
          Last updated at {course?.updatedDate?.toString()}
        </span>
        <span className="italic font-medium text-gray-300">
          Created at {course?.createdDate?.toString()}
        </span>
      </div>
      <div className="mt-8 w-full flex flex-row gap-x-4">
        <Button variant={course?.isFollowing ? "normal" : "outline"}>
          {/* TODO add toggle on whole button - maybe by passing bool prop which will disable the existing onclicks and also useeffect to toggle */}
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
            setUploadModalOpen(true);
          }}
          className="flex flex-row gap-x-2 items-center"
          variant="outline"
        >
          <ImPlus className="text-lg" /> Upload Material
        </Button>
      </div>
      <div className="mt-8 flex flex-col w-full gap-y-8">
        {/* TODO accordion */}
        {renderAccordionSection({
          title: "Staff",
          content: course?.staff,
        })}
        {renderAccordionSection({
          title: "Course Description",
          content: course?.description,
        })}
        {renderAccordionSection({
          title: "Rules",
          content: course?.rules,
        })}
        {/* TODO filter load only posts and notices */}
        {renderAccordionSection({ title: "Posts and Notices", content: "..." })}
        {renderAccordionSection({
          title: "Course Materials",
          content: <CourseMaterials courseId={courseId} />,
        })}
      </div>

      <CourseMaterialUploadRequestModal
        uploadModalOpen={uploadModalOpen}
        materialUploadState={materialUploadState}
        materialUploadDispatch={materialUploadDispatch}
        removeFile={removeFile}
        addFile={addFile}
        upload={upload}
        setUploadModalOpen={setUploadModalOpen}
        clearModalState={clearModalState}
        clearFiles={clearFiles}
      />
    </TitledPageContainer>
  );
};

Course.getLayout = defaultGetLayout;

export default Course;
