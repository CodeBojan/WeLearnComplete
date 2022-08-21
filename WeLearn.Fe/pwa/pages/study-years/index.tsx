import {
  DeleteFollowedStudyYearDto,
  GetFollowedStudyYearDto,
  GetStudyYearDto,
  GetStudyYearDtoPagedResponseDto,
  PostFollowedStudyYearDto,
} from "../../types/api";
import {
  apiFollowedStudyYears,
  apiGetFetcher,
  apiMethodFetcher,
  apiPagedGetFetcher,
  apiStudyYears,
  getPagedApiRouteCacheKey,
  getPagedSearchApiRouteCacheKey,
} from "../../util/api";
import { queryTypes, useQueryState } from "next-usequerystate";
import { useEffect, useState } from "react";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import Button from "../../components/atoms/button";
import FavoritableContainer from "../../components/containers/favoritable-container";
import FavoriteInfo from "../../components/molecules/favorite-info";
import FavoritesContainer from "../../components/containers/favorites-container";
import { MdCalendarToday } from "react-icons/md";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import router from "next/router";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";

const StudyYears: AppPageWithLayout = () => {
  const { data: session, status } = useAppSession();
  const [studyYears, setStudyYears] = useState<GetStudyYearDto[]>([]);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [page, setPage] = useState(1);
  const [onlyMine, setOnlyMine] = useState(false);
  const [mineQueryParam, setMineQueryParam] = useQueryState(
    "mine",
    queryTypes.boolean
  );

  // TODO useSWRInfinite
  // TODO add filtering based on following

  const cacheKey = getPagedSearchApiRouteCacheKey(apiStudyYears, session, {
    page: page.toString(),
    limit: itemsPerPage.toString(),
    isFollowing: onlyMine.toString(),
  });

  const { data: pagedStudyYears, error } =
    useSWR<GetStudyYearDtoPagedResponseDto>(cacheKey, apiGetFetcher);

  useEffect(() => {
    if (mineQueryParam === null) return;
    if (mineQueryParam != onlyMine) setOnlyMine(mineQueryParam);
  }, [mineQueryParam]);

  useEffect(() => {
    if (!pagedStudyYears || !pagedStudyYears.data) return;
    setStudyYears(pagedStudyYears.data);
  }, [pagedStudyYears]);

  return (
    <TitledPageContainer icon={<MdCalendarToday />} title={"Study Years"}>
      <FavoritesContainer className="my-8">
        <div className="my-8">
          <Button
            variant={onlyMine ? "normal" : "outline"}
            padding="large"
            onClick={() => {
              setMineQueryParam(!mineQueryParam);
            }}
          >
            Only Mine
          </Button>
        </div>
        {studyYears.map((studyYear) => (
          <FavoritableContainer key={studyYear.id}>
            <div
              className="flex flex-row gap-x-8 cursor-pointer"
              onClick={() => router.push(`/study-year/${studyYear.id}`)}
            >
              <div>[{studyYear.shortName}]</div>
              <div>{studyYear.fullName}</div>
            </div>
            <FavoriteInfo
              isFollowing={studyYear.isFollowing}
              followerCount={studyYear.followingCount}
              onUnfollow={() => {
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
              onFollow={() => {
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
            />
          </FavoritableContainer>
        ))}
      </FavoritesContainer>
    </TitledPageContainer>
  );
};

StudyYears.getLayout = defaultGetLayout;

export default StudyYears;
