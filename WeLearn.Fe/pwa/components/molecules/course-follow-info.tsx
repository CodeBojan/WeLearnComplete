import { GetFollowedCourseDto, PostFollowedCourseDto } from "../../types/api";
import { apiFollowedCourses, apiMethodFetcher } from "../../util/api";

import FavoriteInfo from "./favorite-info";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";

export default function CourseFollowInfo({
  isFollowing,
  followingCount,
  courseId,
  courseShortName,
  onMutate,
}: {
  isFollowing: boolean | null | undefined;
  followingCount: number | null | undefined;
  courseId: string | null | undefined;
  courseShortName: string | null | undefined;
  onMutate?: () => void;
}) {
  const { data: session } = useAppSession();

  const unfollow = () => {
    apiMethodFetcher(apiFollowedCourses, session.accessToken, "DELETE", {
      accountId: session.user.id,
      courseId: courseId,
    } as PostFollowedCourseDto).then((res) => {
      const resDto = res as GetFollowedCourseDto;
      onMutate && onMutate();
      toast(`Unfollowed ${courseShortName}`, { type: "success" });
    });
  };
  const follow = () => {
    apiMethodFetcher(apiFollowedCourses, session.accessToken, "POST", {
      accountId: session.user.id,
      courseId: courseId,
    } as PostFollowedCourseDto).then((res) => {
      const resDto = res as GetFollowedCourseDto;
      onMutate && onMutate();
      toast(`Followed ${courseShortName}`, { type: "success" });
    });
  };

  // TODO define method for programmatic triggering of follow/unfollow
  return (
    <FavoriteInfo
      isFollowing={isFollowing}
      followerCount={followingCount}
      onUnfollow={() => unfollow()}
      onFollow={() => follow()}
    />
  );
}
