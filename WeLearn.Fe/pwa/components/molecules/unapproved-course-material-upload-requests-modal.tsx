import {
  Accordion,
  AccordionItem,
  AccordionItemButton,
  AccordionItemHeading,
  AccordionItemPanel,
  AccordionItemState,
} from "react-accessible-accordion";
import { MdArrowDropDown, MdArrowDropUp, MdArrowUpward } from "react-icons/md";
import {
  UnapprovedCourseMaterialUploadRequestAction,
  UnapprovedCourseMaterialUploadRequestActionKind,
  UnapprovedCourseMaterialUploadRequestState,
} from "../../store/unapproved-course-material-upload-requests-store";
import {
  apiCourseMaterialUploadRequestApprovers,
  apiMethodFetcher,
} from "../../util/api";

import { AiOutlineSmile } from "react-icons/ai";
import Button from "../atoms/button";
import CustomInfiniteScroll from "./custom-infinite-scroll";
import { Dispatch } from "react";
import { DocumentContainer } from "./document-container";
import EndMessage from "../atoms/end-message";
import InfiniteScroll from "react-infinite-scroll-component";
import Modal from "./modal";
import { ReactNode } from "react";
import { RenderDocument } from "./render-document";
import { RenderPersonalInfo } from "./right-side-bar";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";
import useUnapprovedCourseMaterialUploadRequests from "../../util/useUnapprovedCourseMaterialUploadRequests";

// TODO accept mutate as prop for study materials in main page
export default function UnapprovedCourseMaterialUploadRequestsModal({
  courseId,
  unapprovedRequestsState,
  unapprovedRequestsDispatch,
}: {
  courseId: string;
  unapprovedRequestsState: UnapprovedCourseMaterialUploadRequestState;
  unapprovedRequestsDispatch: Dispatch<UnapprovedCourseMaterialUploadRequestAction>;
}) {
  const { data: session } = useAppSession();
  const { requests, size, setSize, isLoadingMore, isReachingEnd, mutate } =
    useUnapprovedCourseMaterialUploadRequests({ courseId });

  const modalScrollParentId = "unapproved-requests-scroll-parent";
  const hasMore = !isLoadingMore && !isReachingEnd;

  if (!requests) return <></>;

  const approve = (requestId: string) => {
    apiMethodFetcher(
      apiCourseMaterialUploadRequestApprovers(requestId),
      session.accessToken,
      "POST",
      null,
      false
    )
      .then((res) => {
        if (!(res as Response)?.ok) throw new Error();
        mutate();
        toast("Request approved", { type: "success" });
      })
      .catch((err) => {
        toast(`Failed to approve upload request: ${err}`);
      });
  };

  return (
    <div>
      <div className=""></div>
      <Modal
        open={unapprovedRequestsState.modalOpen}
        body={
          <div className="flex flex-col pb-4">
            <div
              className="overflow-auto"
              id={modalScrollParentId}
              style={{ height: "520px" }}
            >
              <CustomInfiniteScroll
                dataLength={requests.length ?? 0}
                next={() => {
                  setSize(size + 1);
                }}
                hasMore={hasMore}
                scrollableTarget={modalScrollParentId}
              >
                <div className="flex flex-col p-4 gap-y-4">
                  {requests?.map((ur, index) => (
                    <div
                      key={ur.id}
                      className="flex flex-row border-b-2 pb-4 justify-between items-center"
                    >
                      <div className="flex flex-col gap-y-4">
                        <div className="text-lg font-bold">{ur.title}</div>
                        <div className="text-lg">{ur.body}</div>

                        <div className="flex flex-row">
                          <Accordion
                            allowZeroExpanded={true}
                            className="w-full"
                          >
                            <AccordionItem>
                              <AccordionItemHeading>
                                <AccordionItemButton>
                                  <div className="flex flex-row items-center gap-x-2 my-2 font-semibold">
                                    Creator
                                    <div className="text-2xl">
                                      <AccordionItemState>
                                        {({ expanded }) =>
                                          expanded ? (
                                            <MdArrowDropUp />
                                          ) : (
                                            <MdArrowDropDown />
                                          )
                                        }
                                      </AccordionItemState>
                                    </div>
                                  </div>
                                </AccordionItemButton>
                              </AccordionItemHeading>
                              <AccordionItemPanel>
                                <div className="border-b-2 py-2">
                                  <RenderPersonalInfo account={ur.creator} />
                                </div>
                              </AccordionItemPanel>
                            </AccordionItem>
                          </Accordion>
                        </div>

                        <div className="flex flex-col">
                          <div>updated at {ur.createdDate?.toString()}</div>
                          <div>created at {ur.updatedDate?.toString()}</div>
                        </div>
                        <div className="">
                          <DocumentContainer
                            documentContainer={ur}
                            session={session}
                          />
                        </div>
                      </div>
                      <div className="flex flex-col justify-center pl-4 md:p-0">
                        <Button onClick={() => ur.id && approve(ur.id)}>
                          Approve
                        </Button>
                      </div>
                    </div>
                  ))}
                </div>
              </CustomInfiniteScroll>
            </div>
          </div>
        }
        showFooter={false}
        footer={<></>}
        header={<>Unapproved Upload Requests</>}
        onTryClose={() =>
          unapprovedRequestsDispatch({
            type: UnapprovedCourseMaterialUploadRequestActionKind.CLOSE_MODAL,
          })
        }
      />
    </div>
  );
}
