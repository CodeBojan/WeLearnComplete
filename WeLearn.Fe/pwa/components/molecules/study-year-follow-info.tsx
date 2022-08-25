import {
  DeleteFollowedStudyYearDto,
  GetFollowedStudyYearDto,
  PostFollowedStudyYearDto,
} from "../../types/api";
import { apiFollowedStudyYears, apiMethodFetcher } from "../../util/api";

import FavoriteInfo from "./favorite-info";
import { mutate } from "swr";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";

// TODO reuse this component in study years listing

export default function StudyYearFollowInfo({
  isFollowing,
  followingCount,
  studyYearId,
  studyYearShortName,
  onMutate,
}: {
  isFollowing: boolean | null | undefined;
  followingCount: number | null | undefined;
  studyYearId: string | null | undefined;
  studyYearShortName: string | null | undefined;
  onMutate?: () => void;
}) {
  const { data: session } = useAppSession();

  const unfollow = () => {
    apiMethodFetcher(apiFollowedStudyYears, session.accessToken, "DELETE", {
      studyYearId: studyYearId,
      accountId: session.user.id,
    } as DeleteFollowedStudyYearDto).then((res) => {
      const resDto = res as GetFollowedStudyYearDto;
      onMutate && onMutate();
      toast(`${studyYearShortName} Unfollowed!`, {
        type: "success",
      });
    });
  };

  const follow = () => {
    apiMethodFetcher(apiFollowedStudyYears, session.accessToken, "POST", {
      studyYearId: studyYearId,
      accountId: session.user.id,
    } as PostFollowedStudyYearDto).then((res) => {
      const resDto = res as GetFollowedStudyYearDto;
      onMutate && onMutate();
      toast(`${studyYearShortName} Followed!`, {
        type: "success",
      });
    });
  };

  return (
    <FavoriteInfo
      isFollowing={isFollowing}
      followerCount={followingCount}
      onUnfollow={() => unfollow()}
      onFollow={() => follow()}
    />
  );
}
