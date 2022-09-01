import ContentCommentsInfo from "./content-comments-info";
import { DocumentContainer } from "./document-container";
import EndMessage from "../atoms/end-message";
import InfiniteScroll from "react-infinite-scroll-component";
import { useAppSession } from "../../util/auth";
import useStudyYearNotices from "../../util/useStudyYearNotices";

export default function StudyYearNotices({
  studyYearId,
}: {
  studyYearId: string;
}) {
  const { data: session } = useAppSession();
  const { studyYearNotices, size, setSize, isLoadingMore, isReachingEnd } =
    useStudyYearNotices({ studyYearId });
  const hasMore = !isReachingEnd && !isLoadingMore;

  // TODO skeletonize
  if (!studyYearNotices) return <></>;

  // TODO use a RenderContent component
  // TODO replace with generic infinite scroll component
  return (
    <InfiniteScroll
      dataLength={studyYearNotices.length ?? 0}
      next={() => {
        setSize(size + 1);
      }}
      hasMore={hasMore}
      loader={<div key={0}>Loading...</div>}
      endMessage={!hasMore && <EndMessage />}
    >
      <div className="flex flex-coll w-full items-start gap-y-4 flex-wrap">
        {studyYearNotices?.map((notice, index) => (
          <div
            key={notice.id}
            className="flex flex-col w-full gap-y-4 p-8 rounded-lg shadow-md"
          >
            <div className="text-xl font-bold">{notice.title}</div>
            <div>{notice.body}</div>
            <div>updated at {notice.createdDate?.toString()}</div>
            <div>created at {notice.updatedDate?.toString()}</div>
            <div>
              {(notice.documentCount ?? 0) > 0 && notice.documents && (
                <DocumentContainer
                  session={session}
                  documentContainer={notice}
                />
              )}
            </div>
            <div className="">
              <ContentCommentsInfo
                contentId={notice.id!}
                commentCount={notice.commentCount}
              />
            </div>
          </div>
        ))}
      </div>
    </InfiniteScroll>
  );
}
