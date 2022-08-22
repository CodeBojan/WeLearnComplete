import {
  AiFillPlusSquare,
  AiFillPushpin,
  AiOutlineCloudUpload,
  AiOutlinePlus,
} from "react-icons/ai";
import {
  GetCourseDto,
  PostCourseMaterialUploadRequestDto,
  PostDocumentDto,
} from "../../types/api";
import { ImCross, ImPlus } from "react-icons/im";
import Input, { InputLabel } from "../../components/atoms/input";
import {
  apiCourse,
  apiCourseMaterialUploadRequestCourse,
  apiGetFetcher,
  apiRoute,
  getApiRouteCacheKey,
} from "../../util/api";
import { useEffect, useState } from "react";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import Button from "../../components/atoms/button";
import CourseFollowInfo from "../../components/molecules/course-follow-info";
import { FileUploader } from "react-drag-drop-files";
import { MdGrading } from "react-icons/md";
import Modal from "../../components/molecules/modal";
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

  function clearModalState() {
    setFiles([]);
    setRequestBody("");
    setRequestRemark("");
  }

  function removeFile(file: File) {
    setFiles(files.filter((f) => f !== file));
  }

  function upload(): void {
    if (files.length === 0) {
      toast.error("You must select at least one file");
      return;
    }
    if (requestBody.length === 0) {
      toast.error("You must enter a request body");
      return;
    }
    if (requestRemark.length === 0) {
      toast.error("You must enter a request remark");
      return;
    }
    const formData = new FormData();
    const dto = {
      body: requestBody,
      remark: requestRemark,
      courseId: courseId,
      documents: files.map((f) => {
        return new PostDocumentDto({
          courseId: courseId,
        });
      }),
    } as PostCourseMaterialUploadRequestDto;
    formData.append("PostDto", JSON.stringify(dto));
    files.forEach((file) => {
      formData.append("Files", file, file.name);
    });
    // TODO extract to api function
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
        <Button>
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
        {/* TODO render each using a render function - 2-3 args */}
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
        {renderAccordionSection({ title: "Course Materials", content: "..." })}
      </div>

      {/* TODO extract upload modal to its own component */}
      <Modal
        open={uploadModalOpen}
        header="Upload Course Material"
        body={
          <div className="m-8 flex flex-col gap-y-4">
            <Input
              label="Description"
              text={requestBody}
              placeholder="2022 Solved Mock Exam"
              onChange={(e) => {
                setRequestBody(e.target.value);
              }}
            ></Input>
            <Input
              label="Remark"
              text={requestRemark}
              placeholder="Please review my solution :)"
              onChange={(e) => {
                setRequestRemark(e.target.value);
              }}
            ></Input>

            <div className="flex flex-col w-full">
              <div className="flex flex-col">
                <InputLabel label="Files" />
                {files && files.length > 0 && (
                  <div className="flex flex-row gap-4">
                    {files.map((file, index) =>
                      renderModalFile(index, file, () => removeFile(file))
                    )}
                  </div>
                )}
              </div>
              <div className="flex justify-center items-center w-full">
                <FileUploader
                  classes="w-full"
                  handleChange={(file: File) => {
                    setFiles([...files, file]);
                    toast(`Added file ${file.name}`, { type: "success" });
                  }}
                >
                  <label className="flex flex-col justify-center items-center w-full h-full bg-gray-50 rounded-lg border-2 border-gray-300 border-dashed cursor-pointer dark:hover:bg-bray-800 dark:bg-gray-700 hover:bg-gray-100 dark:border-gray-600 dark:hover:border-gray-500 dark:hover:bg-gray-600">
                    <div className="flex flex-col justify-center items-center pt-5 pb-6">
                      <AiOutlineCloudUpload className="text-4xl text-gray-500" />
                      <p className="mb-2 text-sm text-gray-500 dark:text-gray-400">
                        <span className="font-semibold">Click to upload</span>{" "}
                        or drag and drop
                      </p>
                      <p className="text-xs text-gray-500 dark:text-gray-400">
                        DOCX, PDF, PNG, JPG ... (MAX. 3.0MB / FILE)
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
          </div>
        }
        footer={
          <div className="mx-4 flex gap-x-4">
            <Button onClick={() => upload()}>Upload</Button>
            <Button
              variant="danger"
              onClick={() => {
                setUploadModalOpen(false);
                clearModalState();
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
function renderModalFile(
  index: number,
  file: File,
  onRemove?: () => void
): JSX.Element {
  return (
    <div
      key={index}
      className="flex flex-row items-center gap-x-2 bg-orange-500 text-white rounded-lg p-2 hover:bg-orange-900 cursor-pointer"
    >
      {file.name}
      <ImCross onClick={() => onRemove && onRemove()} />
    </div>
  );
}
