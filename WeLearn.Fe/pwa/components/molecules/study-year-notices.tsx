import ContentCommentsInfo from "./content-comments-info";
import CustomInfiniteScroll from "./custom-infinite-scroll";
import { DocumentContainer } from "./document-container";
import EndMessage from "../atoms/end-message";
import InfiniteScroll from "react-infinite-scroll-component";
import RenderContent from "./render-content";
import { useAppSession } from "../../util/auth";
import useStudyYearNotices from "../../util/useStudyYearNotices";

export default function StudyYearNotices({
  studyYearId,
}: {
  studyYearId: string;
}) {
  const { data: session } = useAppSession();
  const {
    studyYearNotices,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    hasMore,
  } = useStudyYearNotices({ studyYearId });

  // TODO skeletonize
  if (!studyYearNotices) return <></>;

  return (
    <CustomInfiniteScroll
      dataLength={studyYearNotices.length}
      next={() => {
        setSize(size + 1);
      }}
      hasMore={hasMore}
    >
      <div className="flex flex-col w-full gap-y-4">
        {studyYearNotices?.map((notice, index) => (
          <RenderContent content={notice} key={index} session={session} />
        ))}
      </div>
    </CustomInfiniteScroll>
  );
}
