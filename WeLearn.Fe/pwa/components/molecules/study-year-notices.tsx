import { DocumentContainer } from "./document-container";
import InfiniteScroll from "react-infinite-scroller";
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

  // TODO skeletonize
  if (!studyYearNotices) return <></>;

  return (
    <InfiniteScroll
      pageStart={1}
      loadMore={() => {
        setSize(size + 1);
      }}
      hasMore={!isReachingEnd && !isLoadingMore}
      loader={<div key={0}>Loading...</div>}
    >
      <div className="flex flex-coll w-full items-start gap-y-4">
        {studyYearNotices?.map((notice, index) => (
          <div
            key={notice.id}
            className="flex flex-col w-full gap-y-2 p-4 rounded-lg shadow-md"
          >
            <div className="text-xl font-bold">{notice.title}</div>
            <div>{notice.body}</div>
            <div>updated at {notice.createdDate?.toString()}</div>
            <div>created at {notice.updatedDate?.toString()}</div>
            <div>
              {(notice.documentCount ?? 0) && notice.documents && (
                <DocumentContainer
                  session={session}
                  documentContainer={notice}
                />
              )}
            </div>
          </div>
        ))}
      </div>
    </InfiniteScroll>
  );
}
