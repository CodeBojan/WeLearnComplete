import { AppSession } from "../../types/auth";
import { BiLinkExternal } from "react-icons/bi";
import ContentCommentsInfo from "./content-comments-info";
import { DocumentContainer } from "./document-container";
import EndMessage from "../atoms/end-message";
import { GetContentDto } from "../../types/api";
import InfiniteScroll from "react-infinite-scroll-component";
import Link from "next/link";
import { MdSource } from "react-icons/md";
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
      <InfiniteScroll
        dataLength={content?.length ?? 0}
        next={() => {
          setSize(size + 1);
        }}
        hasMore={hasMore}
        loader={hasMore && <div key={0}>Loading ...</div>}
        endMessage={!hasMore && <EndMessage />}
      >
        <div className="flex flex-col gap-y-4 mt-4">
          {content?.flatMap((c, contentIndex) => (
            <RenderContent key={c.id} content={c} session={session} />
          ))}
        </div>
      </InfiniteScroll>
    </div>
  );
}

function RenderContent({
  content: c,
  session,
}: {
  content: GetContentDto;
  session: AppSession;
}) {
  return (
    <div className="flex flex-col gap-y-2 rounded-lg shadow-md p-8">
      <div className="flex flex-row items-center gap-x-4">
        {c.externalSystem?.logoUrl && (
          <img className="w-8 rounded-lg" src={c.externalSystem?.logoUrl} />
        )}
        <div className="flex flex-col">
          <span className="font-bold">{c.externalSystem?.friendlyName}</span>
          <span className="">{c.author}</span>
        </div>
      </div>
      <div className="text-lg font-semibold">{c.title}</div>
      <div className="">{c.body}</div>
      <DocumentContainer
        documentContainer={{
          documentCount: c.documentCount,
          documents: c.documents,
        }}
        session={session}
      />
      <div className="flex flex-row justify-between mt-4 items-center">
        <ContentCommentsInfo contentId={c.id!} commentCount={c.commentCount} />
        {c.externalUrl && (
          <Link href={c.externalUrl}>
            <a>
              <BiLinkExternal className="text-2xl" />
            </a>
          </Link>
        )}
      </div>
    </div>
  );
}
