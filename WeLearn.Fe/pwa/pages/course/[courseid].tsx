import {
  CourseMaterialUploadRequestAction,
  CourseMaterialUploadRequestActionKind,
  CourseMaterialUploadRequestState,
  courseMaterialUploadRequestReducer,
  initialCourseMaterialUploadRequestState,
} from "../../store/course-material-upload-request-store";
import {
  Dispatch,
  SetStateAction,
  useEffect,
  useReducer,
  useRef,
  useState,
} from "react";
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
import useSWR, { mutate } from "swr";

import { AiOutlineCloudUpload } from "react-icons/ai";
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
  const [materialUploadState, materialUploadDispatch] = useReducer(
    courseMaterialUploadRequestReducer,
    initialCourseMaterialUploadRequestState
  );

  const cacheKey = getApiRouteCacheKey(apiCourse(courseId), session);

  const { data: courseResponse, error } = useSWR<GetCourseDto>(
    cacheKey,
    apiGetFetcher
  );

  // TODO extract to modal component
  // ---------------------------------------------------------

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
      {
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
      }
    </TitledPageContainer>
  );
};

function CourseMaterialUploadRequestModal({
  uploadModalOpen,
  materialUploadState,
  materialUploadDispatch,
  removeFile,
  addFile,
  upload,
  setUploadModalOpen,
  clearModalState,
  clearFiles,
}: {
  uploadModalOpen: boolean;
  materialUploadState: CourseMaterialUploadRequestState;
  materialUploadDispatch: Dispatch<CourseMaterialUploadRequestAction>;
  removeFile: (file: File) => void;
  addFile: (file: File) => void;
  upload: () => void;
  setUploadModalOpen: Dispatch<SetStateAction<boolean>>;
  clearModalState: () => void;
  clearFiles: () => void;
}) {
  function usePrevious(value: File[]) {
    const ref = useRef<File[]>();
    useEffect(() => {
      ref.current = value;
    }, [value]);
    return ref.current;
  }

  const previous = usePrevious(materialUploadState.files);

  useEffect(() => {
    console.log("previous", previous);
    console.log(materialUploadState.files);

    const previousLength = previous?.length ?? 0;
    const diff = materialUploadState.files.length - previousLength;
    if (diff <= 0) return;

    console.log(diff);
    const newFiles = materialUploadState.files.slice(
      previousLength,
      materialUploadState.files.length
    );
    newFiles.forEach((file) => {
      toast(`Added file ${file.name}`, { type: "success" });
    });
  }, [materialUploadState.files]);

  return (
    <Modal
      open={uploadModalOpen}
      header="Upload Course Material"
      body={
        <div className="m-8 flex flex-col gap-y-4">
          <Input
            label="Description"
            text={materialUploadState.body}
            placeholder="2022 Solved Mock Exam"
            onChange={(e) => {
              materialUploadDispatch({
                type: CourseMaterialUploadRequestActionKind.SET_BODY,
                body: e.target.value,
              });
            }}
          ></Input>
          <Input
            label="Remark"
            text={materialUploadState.remark}
            placeholder="Please review my solution :)"
            onChange={(e) => {
              materialUploadDispatch({
                type: CourseMaterialUploadRequestActionKind.SET_REMARK,
                remark: e.target.value,
              });
            }}
          ></Input>

          <div className="flex flex-col w-full">
            <div className="flex flex-col mb-4">
              <InputLabel label="Files" />
              {materialUploadState.files &&
                materialUploadState.files.length > 0 && (
                  <div className="flex flex-row gap-4 flex-wrap">
                    {materialUploadState.files.map((file, index) =>
                      renderModalFile(index, file, () => removeFile(file))
                    )}
                  </div>
                )}
            </div>
            <div className="flex justify-center items-center w-full">
              <FileUploader
                multiple={true}
                classes="w-full"
                handleChange={(files: File | FileList) => {
                  if (files instanceof File) {
                    addFile(files);
                  } else
                    Array.from(files).forEach((file) => {
                      addFile(file);
                    });
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
                      DOCX, PDF, PNG, JPG ... (MAX. 3.0MB / FILE)
                    </p>
                  </div>
                </label>
              </FileUploader>
            </div>
            <div className="mt-4">
              <Button variant="outline" onClick={() => clearFiles()}>
                Clear Files
              </Button>
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
  );
}

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

Course.getLayout = defaultGetLayout;

export default Course;
