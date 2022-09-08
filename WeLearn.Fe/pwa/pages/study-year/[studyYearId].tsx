import {
  AccountSelectorActionKind,
  accountSelectorReducer,
  initialAccountSelectorState,
} from "../../store/account-selector-store";
import {
  CreateCourseAction,
  CreateCourseActionKind,
  CreateCourseState,
  createCourseReducer,
  initialCreateCourseState,
} from "../../store/create-course-store";
import {
  DeleteStudyYearAdminRoleDto,
  PostStudyYearAdminRoleDto,
  WeLearnProblemDetails,
} from "../../types/isApi";
import { Dispatch, useEffect, useReducer, useState } from "react";
import {
  GetAccountDto,
  GetCourseDto,
  GetCourseDtoPagedResponseDto,
  GetStudyYearDto,
  PostCourseDto,
} from "../../types/api";
import {
  MdCalendarViewMonth,
  MdOutlinePlaylistAdd,
  MdPeople,
  MdSubject,
} from "react-icons/md";
import {
  apiCourses,
  apiGetFetcher,
  apiMethodFetcher,
  apiStudyYear,
  getApiRouteCacheKey,
  getPagedSearchApiRouteCacheKey,
} from "../../util/api";
import {
  checkIsStudyYearAdmin,
  checkIsSystemAdmin,
  useAppSession,
} from "../../util/auth";
import {
  isApiMethodFetcher,
  isApiStudyYearAccountRoles,
} from "../../util/isApi";
import { queryTypes, useQueryState } from "next-usequerystate";
import router, { useRouter } from "next/router";
import useSWR, { mutate } from "swr";

import AccountSelectorModal from "../../components/molecules/account-selector-modal";
import { AiFillNotification } from "react-icons/ai";
import { AppPageWithLayout } from "../_app";
import Button from "../../components/atoms/button";
import CoursesList from "../../components/containers/courses-list";
import { CreateCourseModal } from "../../components/molecules/create-course-modal";
import { FaRegCalendarAlt } from "react-icons/fa";
import { GrUserAdmin } from "react-icons/gr";
import Input from "../../components/atoms/input";
import Modal from "../../components/molecules/modal";
import OnlyMineButton from "../../components/atoms/only-mine-button";
import StudyYearAccountSelectorModal from "../../components/molecules/study-year-account-selector";
import StudyYearFollowInfo from "../../components/molecules/study-year-follow-info";
import StudyYearNotices from "../../components/molecules/study-year-notices";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { cache } from "swr/dist/utils/config";
import { defaultGetLayout } from "../../layouts/layout";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";
import { toast } from "react-toastify";

const StudyYear: AppPageWithLayout = () => {
  const { studyYearId } = router.query as { studyYearId: string };
  const { data: session } = useAppSession();
  const isUserStudyYearAdmin = checkIsStudyYearAdmin(session.user, studyYearId);
  const isSystemAdmin = checkIsSystemAdmin(session.user);
  // TODO add isSystemAdmin - only he can make other users study year admins
  const [studyYear, setStudyYear] = useState<GetStudyYearDto | null>(null);

  const cacheKey = getApiRouteCacheKey(apiStudyYear(studyYearId), session);

  const { data: studyYearResponse, error } = useSWR<GetStudyYearDto>(
    cacheKey,
    apiGetFetcher
  );

  // TODO refactor into its own hook/reducer
  const [courses, setCourses] = useState<GetCourseDto[]>([]);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [page, setPage] = useState(1);
  const [onlyMine, setOnlyMine] = useState(false);
  const [mineQueryParam, setMineQueryParam] = useQueryState(
    "mine",
    queryTypes.boolean
  );
  const courseCacheKey = getPagedSearchApiRouteCacheKey(apiCourses, session, {
    page: page.toString(),
    limit: itemsPerPage.toString(),
    isFollowing: onlyMine.toString(),
    studyYearId: studyYearId,
  });

  const [createCourseState, createCourseDispatch] = useReducer(
    createCourseReducer,
    initialCreateCourseState
  );

  const { data: pagedCourses, error: coursesError } =
    useSWR<GetCourseDtoPagedResponseDto>(courseCacheKey, apiGetFetcher);

  const [accountSelectorState, accountSelectorDispatch] = useReducer(
    accountSelectorReducer,
    initialAccountSelectorState
  );

  useEffect(() => {
    if (!studyYearResponse) return;
    setStudyYear(studyYearResponse);
  }, [studyYearResponse]);

  useEffect(() => {
    if (!pagedCourses || !pagedCourses.data) return;
    setCourses(pagedCourses.data);
  }, [pagedCourses]);

  function clearCreateCourseModal() {
    createCourseDispatch({
      type: CreateCourseActionKind.CLEAR,
    });
  }

  function createCourse(): void {
    apiMethodFetcher(apiCourses, session.accessToken, "POST", {
      code: createCourseState.code,
      description: createCourseState.description,
      fullName: createCourseState.fullName,
      shortName: createCourseState.shortName,
      studyYearId: studyYearId,
      rules: createCourseState.rules,
      staff: createCourseState.staff,
    } as PostCourseDto)
      .then((res) => {
        const courseDto = res as GetCourseDto;
        toast(`Course ${courseDto.shortName} created`, { type: "success" });
        mutate(courseCacheKey);
        clearCreateCourseModal();
      })
      .catch((err) => {
        toast(`Failed to create course: ${err.message}`, { type: "error" });
      });
  }

  return (
    <TitledPageContainer
      icon={<FaRegCalendarAlt />}
      title={
        studyYear ? (
          <div className="flex flex-row items-center justify-start gap-x-4">
            {`[${studyYear.shortName}] ${studyYear.fullName}`}
          </div>
        ) : null
      }
    >
      <div className="flex flex-col my-4">
        <div className="italic font-medium text-gray-400">
          updated at {studyYear?.updatedDate?.toString()}
        </div>
        <div>created at {studyYear?.createdDate?.toString()}</div>
      </div>
      <div className="flex flex-row gap-x-4 items-center">
        <Button variant={studyYear?.isFollowing ? "normal" : "outline"}>
          <StudyYearFollowInfo
            isFollowing={studyYear?.isFollowing}
            followingCount={studyYear?.followingCount}
            studyYearId={studyYearId}
            studyYearShortName={studyYear?.shortName}
            onMutate={() => mutate(cacheKey)}
          />
        </Button>
        {isUserStudyYearAdmin && (
          <>
            <GrUserAdmin className="text-xl" />
            <Button
              variant="outline"
              onClick={() =>
                createCourseDispatch({
                  type: CreateCourseActionKind.OPEN_MODAL,
                })
              }
            >
              <div className="flex flex-row items-center gap-x-2">
                <MdOutlinePlaylistAdd className="text-2xl" />{" "}
                <span>Add Course</span>
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
                <MdPeople className="text-2xl" />
                <span>View Users</span>
              </div>
            </Button>
          </>
        )}
      </div>
      <div className="my-8">{studyYear?.description}</div>
      <div className="w-full mb-4 flex flex-col gap-y-4">
        <div className="text-2xl font-bold flex flex-row items-center gap-x-4 ">
          <MdSubject /> Courses
        </div>
        <div className="">
          <div className="mb-8">
            <OnlyMineButton
              onlyMine={onlyMine}
              onClick={() => {
                setOnlyMine(!onlyMine);
                mutate(courseCacheKey);
              }}
            />
          </div>
          <CoursesList
            courses={courses}
            onMutate={() => mutate(courseCacheKey)}
          />
        </div>
      </div>
      <div className="w-full mb-4 flex flex-col gap-y-4">
        <div className="text-2xl font-bold flex flex-row items-center gap-x-4">
          <AiFillNotification />
          Notices
        </div>
        <div className="w-full flex flex-col gap-y-4">
          <StudyYearNotices studyYearId={studyYearId} />
        </div>
      </div>
      <CreateCourseModal
        createCourseState={createCourseState}
        createCourseDispatch={createCourseDispatch}
        createCourse={createCourse}
      />
      {/* TODO extract to component */}
      {isUserStudyYearAdmin && (
        <StudyYearAccountSelectorModal
          accountSelectorDispatch={accountSelectorDispatch}
          accountSelectorState={accountSelectorState}
          studyYearId={studyYearId}
          actionButtons={(account: GetAccountDto, mutate: () => void) => {
            const accountIsAdmin = account.accountRoles?.some(
              (ar) => ar.entityId === studyYearId
            );
            const isCurrentAccount = account.id === session.user.id;
            return (
              <div className="flex flex-row items-center gap-x-2">
                {accountIsAdmin && <GrUserAdmin className="text-xl" />}
                {!isSystemAdmin ? null : !accountIsAdmin ? (
                  <Button
                    disabled={accountIsAdmin}
                    onClick={() => {
                      isApiMethodFetcher(
                        isApiStudyYearAccountRoles,
                        session.accessToken,
                        "POST",
                        {
                          studyYearId: studyYearId,
                          accountId: account.id,
                        } as PostStudyYearAdminRoleDto,
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
                        isApiStudyYearAccountRoles,
                        session.accessToken,
                        "DELETE",
                        {
                          studyYearId: studyYearId,
                          accountId: account.id,
                        } as DeleteStudyYearAdminRoleDto,
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
      )}
    </TitledPageContainer>
  );
};

StudyYear.getLayout = defaultGetLayout;

export async function getStaticProps({ locale }: { locale: string }) {
  return {
    props: {
      ...(await serverSideTranslations(locale, ["common"])),
    },
  };
}

export default StudyYear;
