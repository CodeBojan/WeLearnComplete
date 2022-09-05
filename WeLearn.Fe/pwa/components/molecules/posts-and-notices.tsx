import { AppSession } from "../../types/auth";
import { BiLinkExternal } from "react-icons/bi";
import ContentCommentsInfo from "./content-comments-info";
import CustomInfiniteScroll from "./custom-infinite-scroll";
import { DocumentContainer } from "./document-container";
import EndMessage from "../atoms/end-message";
import { GetContentDto } from "../../types/api";
import InfiniteScroll from "react-infinite-scroll-component";
import Link from "next/link";
import { MdSource } from "react-icons/md";
import RenderContent from "./render-content";
import { useAppSession } from "../../util/auth";
import useCourseContent from "../../util/useCourseContent";

export default function PostsAndNotices({ courseId }: { courseId: string }) {
  const { data: session } = useAppSession();
  const { content, size, setSize, isLoadingMore, isReachingEnd, mutate } =
    useCourseContent({ courseId });

  const hasMore = !isLoadingMore && !isReachingEnd;

  if (!content) return <></>;
  return (
    <div className="flex flex-col gap-y-4">
      <CustomInfiniteScroll
        dataLength={content?.length ?? 0}
        next={() => {
          setSize(size + 1);
        }}
        hasMore={hasMore}
      >
        <div className="flex flex-col gap-y-4 mt-4">
          {content?.flatMap((c, contentIndex) => (
            <RenderContent key={c.id} content={c} session={session} />
          ))}
        </div>
      </CustomInfiniteScroll>
    </div>
  );
}
