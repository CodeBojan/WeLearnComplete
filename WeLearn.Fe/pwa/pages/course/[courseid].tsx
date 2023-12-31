import {
  AccountSelectorActionKind,
  accountSelectorReducer,
  initialAccountSelectorState,
} from "../../store/account-selector-store";
import { AiFillFile, AiFillFileAdd, AiFillNotification } from "react-icons/ai";
import {
  CourseMaterialUploadRequestActionKind,
  courseMaterialUploadRequestReducer,
  initialCourseMaterialUploadRequestState,
} from "../../store/course-material-upload-request-store";
import {
  DeleteCourseAdminRoleDto,
  PostCourseAdminRoleDto,
  WeLearnProblemDetails,
} from "../../types/isApi";
import {
  GetAccountDto,
  GetCourseDto,
  PostCourseMaterialUploadRequestDto,
  PostDocumentDto,
} from "../../types/api";
import { GiRuleBook, GiTeacher } from "react-icons/gi";
import { MdGrading, MdPeople } from "react-icons/md";
import {
  UnapprovedCourseMaterialUploadRequestActionKind,
  initialUnapprovedCourseMaterialUploadRequestState,
  unapprovedCourseMaterialUploadRequestReducer,
} from "../../store/unapproved-course-material-upload-requests-store";
import {
  apiCourse,
  apiCourseMaterialUploadRequestCourse,
  apiGetFetcher,
  apiRoute,
  getApiRouteCacheKey,
} from "../../util/api";
import {
  checkIsCourseAdmin,
  checkIsSystemAdmin,
  useAppSession,
} from "../../util/auth";
import { isApiCourseAccountRoles, isApiMethodFetcher } from "../../util/isApi";
import { useEffect, useReducer, useState } from "react";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import { BiDetail } from "react-icons/bi";
import { BsFillFileEarmarkArrowUpFill } from "react-icons/bs";
import Button from "../../components/atoms/button";
import CourseAccountSelectorModal from "../../components/molecules/course-account-selector-modal";
import CourseFollowInfo from "../../components/molecules/course-follow-info";
import CourseMaterialUploadRequestModal from "../../components/molecules/course-material-upload-request-modal";
import CourseMaterials from "../../components/molecules/course-materials";
import { GrUserAdmin } from "react-icons/gr";
import { ImPlus } from "react-icons/im";
import PostsAndNotices from "../../components/molecules/posts-and-notices";
import { ReactNode } from "react";
import TitledPageContainer from "../../components/containers/titled-page-container";
import UnapprovedCourseMaterialUploadRequestsModal from "../../components/molecules/unapproved-course-material-upload-requests-modal";
import { defaultGetLayout } from "../../layouts/layout";
import router from "next/router";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";
import { toast } from "react-toastify";

// TODO button to go back to study year

const Course: AppPageWithLayout = () => {
  const { courseId: courseId } = router.query as { courseId: string };
  const { data: session } = useAppSession();
  const isSystemAdmin = checkIsSystemAdmin(session.user);
  const isUserCourseAdmin = checkIsCourseAdmin(session.user, courseId);

  const [course, setCourse] = useState<GetCourseDto | null>(null);
  const isCourseAdmin = checkIsCourseAdmin(
    session.user,
    courseId,
    course?.studyYearId
  );
  const [materialUploadState, materialUploadDispatch] = useReducer(
    courseMaterialUploadRequestReducer,
    initialCourseMaterialUploadRequestState
  );

  const cacheKey = getApiRouteCacheKey(apiCourse(courseId), session);

  const { data: courseResponse, error } = useSWR<GetCourseDto>(
    cacheKey,
    apiGetFetcher
  );

  const [uploadModalOpen, setUploadModalOpen] = useState(false);

  const [accountSelectorState, accountSelectorDispatch] = useReducer(
    accountSelectorReducer,
    initialAccountSelectorState
  );

  const [unapprovedRequestsState, unapprovedRequestsDispatch] = useReducer(
    unapprovedCourseMaterialUploadRequestReducer,
    initialUnapprovedCourseMaterialUploadRequestState
  );

  useEffect(() => {
    if (!courseResponse) return;
    setCourse(courseResponse);
  }, [courseResponse]);

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
      title: materialUploadState.title,
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
    fullWidth,
  }: {
    title: ReactNode | null;
    content: ReactNode | null;
    fullWidth?: boolean;
  }) =>
    title &&
    content && (
      <div className={`flex flex-col ${fullWidth ? "w-full" : ""}`}>
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
          updated at {course?.updatedDate?.toString()}
        </span>
        <span className="italic font-medium text-gray-300">
          created at {course?.createdDate?.toString()}
        </span>
      </div>
      <div className="mt-8 w-full flex flex-row flex-wrap gap-y-4 gap-x-4 items-center">
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
          <AiFillFileAdd className="text-2xl" /> Upload Material
        </Button>
        {isCourseAdmin && (
          <>
            <GrUserAdmin className="text-2xl" />
            <Button
              variant="outline"
              onClick={() =>
                unapprovedRequestsDispatch({
                  type: UnapprovedCourseMaterialUploadRequestActionKind.OPEN_MODAL,
                })
              }
            >
              <div className="flex flex-row gap-x-2 items-center">
                <BsFillFileEarmarkArrowUpFill className="text-2xl" />
                <span>Upload Requests</span>
              </div>
            </Button>
            <Button
              variant="outline"
              onClick={() =>
                accountSelectorDispatch({
                  type: AccountSelectorActionKind.OPEN_MODAL,
                })
              }
            >
              <div className="flex flex-row items-center gap-x-2">
                <MdPeople className="text-2xl" /> <span>View Users</span>
              </div>
            </Button>
          </>
        )}
      </div>
      <div className="mt-8 flex flex-col w-full gap-y-8">
        {/* TODO accordion */}
        {/* TODO render function for the headers - or maybe also accept an icon as a param */}
        {renderAccordionSection({
          title: (
            <div className="flex flex-row items-center gap-x-4">
              <GiTeacher />
              <span>Staff</span>
            </div>
          ),
          content: course?.staff,
        })}
        {renderAccordionSection({
          title: (
            <div className="flex flex-row items-center gap-x-4">
              <BiDetail />
              <span>Course Description</span>
            </div>
          ),
          content: course?.description,
        })}
        {renderAccordionSection({
          title: (
            <div className="flex flex-row items-center gap-x-4">
              <GiRuleBook />
              <span>Rules</span>
            </div>
          ),
          content: course?.rules,
        })}
        {renderAccordionSection({
          title: (
            <div className="flex flex-row items-center gap-x-4">
              <AiFillNotification />
              <span>Posts and Notices</span>
            </div>
          ),
          content: <PostsAndNotices courseId={courseId} />,
          fullWidth: true,
        })}
        {renderAccordionSection({
          title: (
            <div className="flex flex-row gap-x-4 items-center">
              <AiFillFile />
              <span>Course Materials</span>
            </div>
          ),
          content: <CourseMaterials courseId={courseId} />,
          fullWidth: true,
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
      {isCourseAdmin && (
        <>
          <CourseAccountSelectorModal
            accountSelectorState={accountSelectorState}
            accountSelectorDispatch={accountSelectorDispatch}
            courseId={courseId}
            actionButtons={(account: GetAccountDto, mutate: () => void) => {
              const accountIsCourseAdmin = account.accountRoles?.some(
                (ar) => ar.entityId === courseId
              );
              const isCurrentAccount = account.id === session.user.id;
              return (
                <div className="flex flex-row items-center gap-x-2">
                  {accountIsCourseAdmin && <GrUserAdmin className="text-xl" />}
                  {!accountIsCourseAdmin ? (
                    <Button
                      disabled={accountIsCourseAdmin}
                      onClick={() => {
                        isApiMethodFetcher(
                          isApiCourseAccountRoles,
                          session.accessToken,
                          "POST",
                          {
                            courseId: courseId,
                            accountId: account.id,
                          } as PostCourseAdminRoleDto,
                          false
                        )
                          .then((res) => {
                            mutate();
                            toast("Admin role added", { type: "success" });
                          })
                          .catch((err) => {
                            const pd = err as Promise<WeLearnProblemDetails>;
                            pd.then((details) => {
                              toast(
                                `Failed to make user admin: ${details.detail}`,
                                {
                                  type: "error",
                                }
                              );
                            });
                          });
                      }}
                    >
                      Make Admin
                    </Button>
                  ) : (
                    <Button
                      variant="danger"
                      disabled={isCurrentAccount}
                      onClick={() => {
                        isApiMethodFetcher(
                          isApiCourseAccountRoles,
                          session.accessToken,
                          "DELETE",
                          {
                            courseId: courseId,
                            accountId: account.id,
                          } as DeleteCourseAdminRoleDto,
                          false
                        )
                          .then((res) => {
                            mutate();
                            toast("Admin role removed", { type: "success" });
                          })
                          .catch((err) => {
                            if (!err) return;
                            if (err instanceof Promise) {
                              err.then((details) => {
                                toast(
                                  `Failed to remove admin role: ${details.detail}`,
                                  {
                                    type: "error",
                                  }
                                );
                              });
                            }
                          });
                      }}
                    >
                      Remove Admin
                    </Button>
                  )}
                </div>
              );
            }}
          />
          <UnapprovedCourseMaterialUploadRequestsModal
            unapprovedRequestsState={unapprovedRequestsState}
            unapprovedRequestsDispatch={unapprovedRequestsDispatch}
            courseId={courseId}
          />
        </>
      )}
    </TitledPageContainer>
  );
};

Course.getLayout = defaultGetLayout;

export async function getServerSideProps({ locale }: { locale: string }) {
  return {
    props: {
      ...(await serverSideTranslations(locale, ["common"])),
    },
  };
}

export default Course;
