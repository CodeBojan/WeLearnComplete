import {
  AiFillPlusSquare,
  AiFillPushpin,
  AiOutlineCloudUpload,
  AiOutlinePlus,
} from "react-icons/ai";
import { apiCourse, apiGetFetcher, getApiRouteCacheKey } from "../../util/api";
import { useEffect, useState } from "react";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import Button from "../../components/atoms/button";
import CourseFollowInfo from "../../components/molecules/course-follow-info";
import { FileUploader } from "react-drag-drop-files";
import { GetCourseDto } from "../../types/api";
import { ImPlus } from "react-icons/im";
import Input from "../../components/atoms/input";
import { MdGrading } from "react-icons/md";
import Modal from "../../components/molecules/modal";
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

  // TODO extract to modal component
  const [requestBody, setRequestBody] = useState("");
  const [requestRemark, setRequestRemark] = useState("");
  const [files, setFiles] = useState<File[]>([]);
  // ---------------------------------------------------------

  useEffect(() => {
    if (!courseResponse) return;
    setCourse(courseResponse);
  }, [courseResponse]);

  const [uploadModalOpen, setUploadModalOpen] = useState(false);

  function upload(): void {
    throw new Error("Function not implemented.");
  }

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
        {/* TODO render each using a render function - 2-3 args */}
        {course?.staff && (
          <div className="flex flex-col">
            <span className="text-2xl font-bold">Staff</span>
            <span className="text-lg">{course?.staff}</span>
          </div>
        )}
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

      {/* TODO extract upload modal to its own component */}
      <Modal
        open={uploadModalOpen}
        header="Upload Course Material"
        body={
          <div className="m-8 flex flex-col gap-y-4">
            <Input
              label="Description"
              text={""}
              placeholder="2022 Solved Mock Exam"
              onChange={() => {}}
            ></Input>
            <Input
              label="Remark"
              text={""}
              placeholder="Please review my solution :)"
              onChange={() => {}}
            ></Input>

            {files && files.length > 0 && (
              <div className="flex flex-row gap-x-4">
                {files.map((file, index) => (
                  <div key={index}>{file.name}</div>
                  // TODO show x button to remove file
                ))}
              </div>
            )}
            <div className="flex justify-center items-center w-full">
              <FileUploader
                classes="w-full"
                handleChange={(file: File) => {
                  // TODO check if file already exists
                  return setFiles([...files, file]);
                }}
              >
                <label className="flex flex-col justify-center items-center w-full h-full bg-gray-50 rounded-lg border-2 border-gray-300 border-dashed cursor-pointer dark:hover:bg-bray-800 dark:bg-gray-700 hover:bg-gray-100 dark:border-gray-600 dark:hover:border-gray-500 dark:hover:bg-gray-600">
                  <div className="flex flex-col justify-center items-center pt-5 pb-6">
                    <AiOutlineCloudUpload className="text-4xl text-gray-500" />
                    <p className="mb-2 text-sm text-gray-500 dark:text-gray-400">
                      <span className="font-semibold">Click to upload</span> or
                      drag and drop
                    </p>
                    <p className="text-xs text-gray-500 dark:text-gray-400">
                      DOCX,PDF, PNG, JPG ... (MAX. 3.0MB / FILE)
                    </p>
                  </div>
                  <input
                    type="file"
                    onChange={(e) => console.log(e)}
                    multiple={true}
                    className="hidden"
                  />
                </label>
              </FileUploader>
            </div>
          </div>
        }
        footer={
          <div className="mx-6 flex gap-x-4">
            <Button onClick={() => upload()}> Upload</Button>
            <Button
              variant="danger"
              onClick={() => {
                // TODO clear previous state
                setUploadModalOpen(false);
                setRequestBody("");
                setRequestRemark("");
              }}
            >
              Cancel
            </Button>
          </div>
        }
        onTryClose={() => setUploadModalOpen(false)}
      />
    </TitledPageContainer>
  );
};

Course.getLayout = defaultGetLayout;

export default Course;
