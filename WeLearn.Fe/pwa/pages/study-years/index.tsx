import { AiFillHeart, AiOutlineHeart } from "react-icons/ai";
import {
  DeleteFollowedStudyYearDto,
  GetFollowedStudyYearDto,
  GetStudyYearDto,
  GetStudyYearDtoPagedResponseDto,
  PostFollowedStudyYearDto,
} from "../../types/api";
import Layout, { defaultGetLayout } from "../../layouts/layout";
import { ReactElement, useEffect, useState } from "react";
import {
  apiFollowedStudyYears,
  apiGetFetcher,
  apiMethodFetcher,
  apiPagedGetFetcher,
  apiSearchGetFetcher,
  apiStudyYears,
  getApiRouteCacheKey,
  getPagedApiRouteCacheKey,
} from "../../util/api";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import IconButton from "../../components/atoms/icon-button";
import { MdCalendarToday } from "react-icons/md";
import { cache } from "swr/dist/utils/config";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";

const StudyYears: AppPageWithLayout = () => {
  const { data: session, status } = useAppSession();
  const [studyYears, setStudyYears] = useState<GetStudyYearDto[]>([]);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [page, setPage] = useState(1);

  const cacheKey = getPagedApiRouteCacheKey(
    apiStudyYears,
    session,
    page,
    itemsPerPage
  );

  const { data: pagedStudyYears, error } =
    useSWR<GetStudyYearDtoPagedResponseDto>(cacheKey, apiPagedGetFetcher);

  useEffect(() => {
    if (!pagedStudyYears || !pagedStudyYears.data) return;
    setStudyYears(pagedStudyYears.data);
  }, [pagedStudyYears]);

  return (
    <div className="grow flex flex-col w-full items-start justify-start mt-20 px-8">
      <div className="flex flex-row gap-x-8 items-center text-4xl font-bold">
        <div>
          <MdCalendarToday />
        </div>
        <div>Study Years</div>
      </div>
      <div className="w-full flex flex-col justify-start mt-8 gap-y-8">
        {studyYears.map((studyYear) => (
          <div
            key={studyYear.id}
            className="grow flex flex-row text-xl font-semibold items-center md:justify-start  justify-between
            hover:bg-slate-500 hover:bg-opacity-20
            hover:cursor-pointer
            py-2 px-4 rounded"
          >
            <div className="flex flex-row gap-x-8">
              [{studyYear.shortName}]<div>{studyYear.fullName}</div>
            </div>
            <div className="flex flex-row items-center gap-x-4">
              <span>{studyYear.following}</span>

              {studyYear.following ? (
                <IconButton
                  onClick={() => {
                    apiMethodFetcher(
                      apiFollowedStudyYears,
                      session.accessToken,
                      "DELETE",
                      {
                        studyYearId: studyYear.id,
                        accountId: session.user.id,
                      } as DeleteFollowedStudyYearDto
                    ).then((res) => {
                      const resDto = res as GetFollowedStudyYearDto;
                      mutate(cacheKey);
                      toast(`${studyYear.shortName} Unfollowed!`, {
                        type: "success",
                      });
                    });
                  }}
                >
                  <AiFillHeart />
                </IconButton>
              ) : (
                <IconButton
                  onClick={() => {
                    apiMethodFetcher(
                      apiFollowedStudyYears,
                      session.accessToken,
                      "POST",
                      {
                        studyYearId: studyYear.id,
                        accountId: session.user.id,
                      } as PostFollowedStudyYearDto
                    ).then((res) => {
                      const resDto = res as GetFollowedStudyYearDto;
                      mutate(cacheKey);
                      toast(`${studyYear.shortName} Followed!`, {
                        type: "success",
                      });
                    });
                  }}
                >
                  <AiOutlineHeart />
                </IconButton>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

StudyYears.getLayout = defaultGetLayout;

export default StudyYears;
