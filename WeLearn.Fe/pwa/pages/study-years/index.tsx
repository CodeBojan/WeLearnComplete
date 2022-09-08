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
import { checkIsStudyYearAdmin, useAppSession } from "../../util/auth";
import { queryTypes, useQueryState } from "next-usequerystate";
import { useEffect, useState } from "react";
import useSWR, { mutate } from "swr";

import { AppPageWithLayout } from "../_app";
import Button from "../../components/atoms/button";
import CustomInfiniteScroll from "../../components/molecules/custom-infinite-scroll";
import FavoritableContainer from "../../components/containers/favoritable-container";
import FavoriteInfo from "../../components/molecules/favorite-info";
import FavoritesContainer from "../../components/containers/favorites-container";
import { GrUserAdmin } from "react-icons/gr";
import { MdCalendarToday } from "react-icons/md";
import OnlyMineButton from "../../components/atoms/only-mine-button";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import router from "next/router";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";
import { toast } from "react-toastify";
import useStudyYears from "../../util/useStudyYears";

const StudyYears: AppPageWithLayout = () => {
  const { data: session, status } = useAppSession();
  const [onlyMine, setOnlyMine] = useState(false);
  const [mineQueryParam, setMineQueryParam] = useQueryState(
    "mine",
    queryTypes.boolean
  );
  const {
    studyYears,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
    hasMore,
  } = useStudyYears({ followingOnly: onlyMine });

  useEffect(() => {
    if (mineQueryParam === null) return;
    if (mineQueryParam != onlyMine) setOnlyMine(mineQueryParam);
  }, [mineQueryParam]);

  const onFollow = (
    studyYearId: string | undefined,
    studyYearShortName: string | undefined
  ) => {
    apiMethodFetcher(apiFollowedStudyYears, session.accessToken, "POST", {
      studyYearId: studyYearId,
      accountId: session.user.id,
    } as PostFollowedStudyYearDto).then((res) => {
      const resDto = res as GetFollowedStudyYearDto;
      mutate();
      toast(`${studyYearShortName} Followed!`, {
        type: "success",
      });
    });
  };

  const onUnfollow = (
    studyYearId: string | undefined,
    studyYearShortName: string | undefined
  ) => {
    apiMethodFetcher(apiFollowedStudyYears, session.accessToken, "DELETE", {
      studyYearId: studyYearId,
      accountId: session.user.id,
    } as DeleteFollowedStudyYearDto).then((res) => {
      const resDto = res as GetFollowedStudyYearDto;
      mutate();
      toast(`${studyYearShortName} Unfollowed!`, {
        type: "success",
      });
    });
  };

  return (
    <TitledPageContainer icon={<MdCalendarToday />} title={"Study Years"}>
      <FavoritesContainer className="my-4">
        <div className="">
          <OnlyMineButton
            onlyMine={onlyMine}
            onClick={() => setMineQueryParam(!mineQueryParam)}
          />
        </div>
        <CustomInfiniteScroll
          dataLength={studyYears?.length}
          next={() => setSize(size + 1)}
          hasMore={hasMore}
          showLoader={isLoadingMore}
        >
          <FavoritesContainer>
            {studyYears?.map((studyYear) => (
              <FavoritableContainer key={studyYear.id}>
                <div
                  className="flex flex-row gap-x-8 cursor-pointer items-center"
                  onClick={() => router.push(`/study-year/${studyYear.id}`)}
                >
                  {checkIsStudyYearAdmin(session.user, studyYear.id!) && (
                    <GrUserAdmin className="text-2xl" />
                  )}
                  <div>[{studyYear.shortName}]</div>
                  <div>{studyYear.fullName}</div>
                </div>
                <FavoriteInfo
                  isFollowing={studyYear.isFollowing}
                  followerCount={studyYear.followingCount}
                  onUnfollow={() =>
                    onUnfollow(studyYear.id, studyYear.shortName)
                  }
                  onFollow={() => onFollow(studyYear.id, studyYear.shortName)}
                />
              </FavoritableContainer>
            ))}
          </FavoritesContainer>
        </CustomInfiniteScroll>
      </FavoritesContainer>
    </TitledPageContainer>
  );
};

StudyYears.getLayout = defaultGetLayout;

export async function getServerSideProps({ locale }: { locale: string }) {
  return {
    props: {
      ...(await serverSideTranslations(locale, ["common"])),
    },
  };
}

export default StudyYears;
