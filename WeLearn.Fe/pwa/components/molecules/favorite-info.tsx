import { AiFillHeart, AiOutlineHeart } from "react-icons/ai";

import FavoritableContainer from "../containers/favoritable-container";
import FavoriteInfoContainer from "../containers/favorite-info-container";
import IconButton from "../atoms/icon-button";

export default function FavoriteInfo({
  isFollowing,
  followerCount,
  onFollow,
  onUnfollow,
}: {
  isFollowing: boolean | null | undefined;
  followerCount: number | null | undefined;
  onFollow: () => void;
  onUnfollow: () => void;
}) {
  console.log(isFollowing);
  return (
    <FavoriteInfoContainer>
      <span>{followerCount}</span>
      {isFollowing ? (
        <IconButton onClick={onUnfollow}>
          <AiFillHeart />
        </IconButton>
      ) : (
        <IconButton onClick={onFollow}>
          <AiOutlineHeart />
        </IconButton>
      )}
    </FavoriteInfoContainer>
  );
}
