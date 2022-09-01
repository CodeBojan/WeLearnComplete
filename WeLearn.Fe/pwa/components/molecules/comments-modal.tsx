import {
  CommentsModalActionKind,
  CommentsModalContext,
} from "../../store/comments-modal-store";
import { GetCommentDto, PostCommentDto } from "../../types/api";
import { apiContentComments, apiMethodFetcher } from "../../util/api";
import { useContext, useRef } from "react";

import { AiOutlineSmile } from "react-icons/ai";
import Button from "../atoms/button";
import { CommentsInvalidationContext } from "../../store/comment-invalidation-context";
import EndMessage from "../atoms/end-message";
import InfiniteScroll from "react-infinite-scroll-component";
import Input from "../atoms/input";
import Modal from "./modal";
import { mutate } from "swr";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";
import useContentComments from "../../util/useContentComments";

export default function CommentsModal({}: {}) {
  const { data: session } = useAppSession();
  const { state: commentsModalContext, dispatch: commentsModalDispatch } =
    useContext(CommentsModalContext);

  // TODO add comments invalidation
  const {
    comments,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    isValidating,
    mutate,
  } = useContentComments({ contentId: commentsModalContext.contentId });
  const contentId = commentsModalContext.contentId;

  const addComment = () =>
    contentId &&
    apiMethodFetcher(
      apiContentComments(contentId),
      session.accessToken,
      "POST",
      {
        authorId: session.user.id,
        contentId: contentId,
        body: commentsModalContext.commentText,
      } as PostCommentDto
    ).then((res) => {
      const dto = res as GetCommentDto;
      mutate();
      toast(`Comment created`, { type: "success" });
      commentsModalDispatch({
        type: CommentsModalActionKind.SET_COMMENT_TEXT,
        payload: "",
      });
    });

  const hasMore = !isLoadingMore && !isReachingEnd;
  const modalScrollParentId = "comments-modal-scroll-parent";
  return (
    <Modal
      open={commentsModalContext.isOpen}
      onTryClose={() => {
        commentsModalDispatch({
          type: CommentsModalActionKind.CLOSE_MODAL,
        });
      }}
      body={
        <div className="flex flex-col gap-y-4 p-4">
          <div className="flex flex-col gap-y-4">
            <Input
              label="Text"
              text={commentsModalContext.commentText}
              placeholder="Comment text..."
              onChange={(e) => {
                commentsModalDispatch({
                  type: CommentsModalActionKind.SET_COMMENT_TEXT,
                  payload: e.target.value,
                });
              }}
            ></Input>
            <div>
              <Button
                disabled={
                  !commentsModalContext.commentText ||
                  commentsModalContext.commentText.length === 0
                }
                onClick={() => addComment()}
              >
                Submit
              </Button>
            </div>
          </div>
          <div className="flex flex-col">
            <div className="text-md font-semibold">Latest Comments</div>
            <div
              id={modalScrollParentId}
              className="overflow-auto"
              style={{ height: "520px" }}
            >
              <InfiniteScroll
                dataLength={comments?.length ?? 0}
                next={() => {
                  return setSize(size + 1);
                }}
                hasMore={hasMore}
                loader={hasMore && <div key={0}>Loading...</div>}
                scrollableTarget={modalScrollParentId}
                endMessage={!hasMore && <EndMessage />}
              >
                <div className="">
                  {comments?.map((comment, index) => (
                    <div
                      key={comment.id}
                      className="flex flex-col border-b-2 p-4"
                    >
                      {/* TODO improve */}
                      <div className="flex flex-row gap-x-2 items-center">
                        <div className="font-semibold">
                          {comment.author?.username}
                        </div>
                        <div className="text-sm">
                          ({comment.author?.facultyStudentId})
                        </div>
                      </div>
                      <div className="text-lg font-semibold">
                        {comment.body}
                      </div>
                      {comment.updatedDate != comment.createdDate && (
                        <div className="text-sm italic text-gray-400">
                          updated at{comment.updatedDate?.toString()}
                        </div>
                      )}
                      <div className="text-sm italic text-gray-600">
                        at {comment.createdDate?.toString()}
                      </div>
                    </div>
                  ))}
                </div>
              </InfiniteScroll>
            </div>
          </div>
        </div>
      }
      footer={<></>}
      // TODO add title for comments to store
      header={<>Comments</>}
      showFooter={false}
    />
  );
}
