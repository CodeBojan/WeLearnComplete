import {
  GetStudyMaterialDto,
  GetStudyMaterialDtoPagedResponseDto,
} from "../../types/api";
import { MdComment, MdSource } from "react-icons/md";
import {
  apiGetFetcher,
  apiStudyMaterialsCourse,
  getApiSWRInfiniteKey,
  processSWRInfiniteData,
} from "../../util/api";
import { useEffect, useState } from "react";

import { AppSession } from "../../types/auth";
import ContentCommentsInfo from "./content-comments-info";
import { DocumentContainer } from "./document-container";
import EndMessage from "../atoms/end-message";
import InfiniteScroll from "react-infinite-scroll-component";
import { useAppSession } from "../../util/auth";
import useCourseStudyMaterials from "../../util/useCourseStudyMaterials";
import useSWRInfinite from "swr/infinite";

export default function CourseMaterials({ courseId }: { courseId: string }) {
  const { data: session } = useAppSession();
  const {
    studyMaterials,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
  } = useCourseStudyMaterials({ courseId });

  const hasMore = !isLoadingMore && !isReachingEnd; // TODO refactor into returned bool

  if (!studyMaterials) return <></>;

  // TODO fix infinite scroll
  return (
    <InfiniteScroll
      dataLength={studyMaterials?.length ?? 0}
      next={() => {
        setSize(size + 1);
      }}
      hasMore={hasMore}
      loader={hasMore && <div key={0}>Loading ...</div>}
      endMessage={!hasMore && <EndMessage />}
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
    </InfiniteScroll>
  );
}

// TODO use RenderContent component
function RenderStudyMaterial({
  studyMaterial: sm,
  session,
}: {
  studyMaterial: GetStudyMaterialDto;
  session: AppSession;
}) {
  return (
    <div>
      <div className="flex flex-col gap-y-2 rounded-lg shadow-md p-8">
        <div className="flex flex-row gap-x-4">
          {/* TODO logo or external image uri*/}
          <div className="text-xl font-semibold">{sm.title}</div>
        </div>
        <div className="text-md">{sm.body}</div>
        <div className="flex flex-row gap-x-2">
          <span>
            {sm.creator?.username}
            {sm.creator?.facultyStudentId && (
              <span className="text-sm"> ({sm.creator.facultyStudentId})</span>
            )}
          </span>
        </div>
        <div className="flex flex-col gap-y-1 text-sm text-gray-400">
          <div>updated at {sm.createdDate?.toString()}</div>
          <div>created at {sm.updatedDate?.toString()}</div>
        </div>
        <RenderStudyMaterialDocuments studyMaterial={sm} session={session} />
        <div className="flex flex-row justify-between mt-4 items-center">
          <ContentCommentsInfo
            commentCount={sm.commentCount}
            contentId={sm.id!}
          />
          {sm.externalUrl && (
            <div>
              <MdSource className="text-2xl" />
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

function RenderStudyMaterialDocuments({
  studyMaterial: sm,
  session,
}: {
  studyMaterial: GetStudyMaterialDto;
  session: AppSession;
}) {
  return (
    <div>
      {/* TODO change course material upload request to have title and body and remark, while not requiring files - the body can contain useful links */}
      {(sm.documentCount ?? 0) > 0 && sm.documents && (
        <DocumentContainer documentContainer={sm} session={session} />
      )}
    </div>
  );
}
