import {
  CommentsModalAction,
  CommentsModalActionKind,
  CommentsModalContext,
  CommentsModalState,
} from "../../store/comments-modal-store";
import { Dispatch, useContext } from "react";
import { MdComment, MdSource } from "react-icons/md";

import { AppSession } from "../../types/auth";
import { DocumentContainer } from "./document-container";
import { GetStudyMaterialDto } from "../../types/api";

export default function ContentCommentsInfo({
  contentId,
  commentCount,
}: {
  contentId: string;
  commentCount: number | null | undefined;
}) {
  const { state: commentsModalContext, dispatch: commentsModalDispatch } =
    useContext(CommentsModalContext);

  return (
    <RenderCommentsButton
      contentId={contentId}
      commentCount={commentCount}
      commentsModalContext={commentsModalContext}
      commentsModalDispatch={commentsModalDispatch}
    />
  );
}

function RenderCommentsButton({
  contentId,
  commentCount,
  commentsModalContext,
  commentsModalDispatch,
}: {
  contentId: string;
  commentCount: number | null | undefined;
  commentsModalContext: CommentsModalState;
  commentsModalDispatch: Dispatch<CommentsModalAction>;
}) {
  return (
    <div
      className="flex flex-row gap-x-4 items-center hover:bg-gray-200 rounded-lg p-2 cursor-pointer"
      onClick={() => {
        if (commentsModalContext.contentId != contentId) {
          commentsModalDispatch({
            type: CommentsModalActionKind.CLEAR,
          });
          commentsModalDispatch({
            type: CommentsModalActionKind.SET_CONTENT_ID,
            payload: contentId,
          });
        }
        commentsModalDispatch({
          type: CommentsModalActionKind.OPEN_MODAL,
        });
      }}
    >
      {commentCount && <span className="">{commentCount}</span>}
      <MdComment className="text-2xl" />
    </div>
  );
}
