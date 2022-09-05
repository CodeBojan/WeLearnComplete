import { useContext, useEffect } from "react";

import { AppSession } from "../../types/auth";
import { CommentsInvalidationContext } from "../../store/comments-invalidation-context";
import CustomInfiniteScroll from "./custom-infinite-scroll";
import { GetStudyMaterialDto } from "../../types/api";
import RenderContent from "./render-content";
import { useAppSession } from "../../util/auth";
import useCourseStudyMaterials from "../../util/useCourseStudyMaterials";

// TODO can't have more than one infinite scroll targeting body
export default function CourseMaterials({ courseId }: { courseId: string }) {
  const { data: session } = useAppSession();
  const { commentsInvalidationState } = useContext(CommentsInvalidationContext);
  const {
    studyMaterials,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
    hasMore,
  } = useCourseStudyMaterials({ courseId });

  // TODO complete comments invalidation
  useEffect(() => {
    mutate();
  }, [commentsInvalidationState.lastInvalidated]);

  if (!studyMaterials) return <></>;

  return (
    <CustomInfiniteScroll
      dataLength={studyMaterials?.length ?? 0}
      next={() => {
        setSize(size + 1);
      }}
      hasMore={hasMore}
    >
      <div className="flex flex-col gap-y-4 mt-4">
        {studyMaterials?.flatMap((sm, studyMaterialIndex) => (
          <RenderStudyMaterial
            key={sm.id}
            studyMaterial={sm}
            session={session}
          />
        ))}
      </div>
    </CustomInfiniteScroll>
  );
}

function RenderStudyMaterial({
  studyMaterial: sm,
  session,
}: {
  studyMaterial: GetStudyMaterialDto;
  session: AppSession;
}) {
  return <RenderContent content={sm} session={session} />;
}
