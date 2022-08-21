import {
  DeleteFollowedStudyYearDto,
  GetFollowedStudyYearDto,
  GetStudyYearDto,
  GetStudyYearDtoPagedResponseDto,
  PostFollowedStudyYearDto,
} from "../../types/api";
import {
  apiFollowedStudyYears,
  apiMethodFetcher,
  apiPagedGetFetcher,
  apiStudyYears,
  getPagedApiRouteCacheKey,
} from "../../util/api";
import { useEffect, useState } from "react";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import FavoritableContainer from "../../components/containers/favoritable-container";
import FavoriteInfo from "../../components/molecules/favorite-info";
import FavoritesContainer from "../../components/containers/favorites-container";
import { MdCalendarToday } from "react-icons/md";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";

const StudyYears: AppPageWithLayout = () => {
  const { data: session, status } = useAppSession();
  const [studyYears, setStudyYears] = useState<GetStudyYearDto[]>([]);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [page, setPage] = useState(1);

  // TODO useSWRInfinite
  // TODO add filtering based on following

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
    <TitledPageContainer icon={<MdCalendarToday />} title={"Study Years"}>
      <FavoritesContainer>
        {studyYears.map((studyYear) => (
          <FavoritableContainer key={studyYear.id}>
            <div className="flex flex-row gap-x-8">
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
