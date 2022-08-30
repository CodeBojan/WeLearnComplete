import {
  UnapprovedCourseMaterialUploadRequestAction,
  UnapprovedCourseMaterialUploadRequestActionKind,
  UnapprovedCourseMaterialUploadRequestState,
} from "../../store/unapproved-course-material-upload-requests-store";

import { Dispatch } from "react";
import InfiniteScroll from "react-infinite-scroller";
import Modal from "./modal";
import { ReactNode } from "react";
import useUnapprovedCourseMaterialUploadRequests from "../../util/useUnapprovedCourseMaterialUploadRequests";

export default function UnapprovedCourseMaterialUploadRequestsModal({
  courseId,
  unapprovedRequestsState,
  unapprovedRequestsDispatch,
}: {
  courseId: string;
  unapprovedRequestsState: UnapprovedCourseMaterialUploadRequestState;
  unapprovedRequestsDispatch: Dispatch<UnapprovedCourseMaterialUploadRequestAction>;
}) {
  const { requests, size, setSize, isLoadingMore, isReachingEnd } =
    useUnapprovedCourseMaterialUploadRequests({ courseId });

  if (!requests) return <></>;

  return (
    <>
      <div className=""></div>
      <Modal
        open={false} // TODO: fix
        body={
          <InfiniteScroll
            pageStart={1}
            loadMore={() => {
              setSize(size + 1);
            }}
            hasMore={!isReachingEnd && !isLoadingMore}
            loader={<div key={0}>Loading ...</div>}
          >
            <div className="flex flex-col gap-y-4">
              {requests?.map((ur, index) => (
                <div key={ur.id} className="">
                  <div>{ur.createdDate?.toString()}</div>
                  <div>{ur.updatedDate?.toString()}</div>
                </div>
              ))}
            </div>
          </InfiniteScroll>
        }
        footer={<></>}
        header={<>Unapproved Upload Requests</>}
        onTryClose={() =>
          unapprovedRequestsDispatch({
            type: UnapprovedCourseMaterialUploadRequestActionKind.CLOSE_MODAL,
          })
        }
      />
    </>
  );
}
