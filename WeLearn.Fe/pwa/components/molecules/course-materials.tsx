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
import InfiniteScroll from "react-infinite-scroller";
import { useAppSession } from "../../util/auth";
import useSWRInfinite from "swr/infinite";

export default function CourseMaterials({ courseId }: { courseId: string }) {
  const [pageSize, setPageSize] = useState(2); // TODO
  const { data: session } = useAppSession();
  const [studyMaterials, setStudyMaterials] = useState<
    GetStudyMaterialDto[] | null
  >(null);

  const getKey = getApiSWRInfiniteKey({
    url: apiStudyMaterialsCourse(courseId),
    session: session,
    pageSize: pageSize,
  });

  const {
    data: pagesDtos,
    error,
    isValidating,
    mutate,
    size,
    setSize,
  } = useSWRInfinite<GetStudyMaterialDtoPagedResponseDto>(
    getKey,
    apiGetFetcher,
    { revalidateAll: true }
  );

  const { isLoadingMore, isReachingEnd } = processSWRInfiniteData(
    size,
    pageSize,
    isValidating,
    error,
    pagesDtos
  );

  useEffect(() => {
    if (!pagesDtos) return;
    const studyMaterialMap = new Map<string, GetStudyMaterialDto>();
    pagesDtos.forEach((pageDto) => {
      pageDto.data?.forEach((studyMaterial) => {
        studyMaterial.id &&
          studyMaterialMap.set(studyMaterial.id, studyMaterial);
      });
    });

    setStudyMaterials(Array.from(studyMaterialMap.values()));
  }, [pagesDtos]);

  if (!studyMaterials) return <></>;

  return (
    <InfiniteScroll
      pageStart={1}
      loadMore={() => {
        setSize(size + 1);
      }}
      hasMore={!isLoadingMore && !isReachingEnd}
      loader={<div key={0}>Loading ...</div>}
    >
      <div className="flex flex-col gap-y-4 mt-4">
        {pagesDtos &&
          studyMaterials?.flatMap((sm, studyMaterialIndex) => (
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
        <div className="flex flex-col gap-y-1 text-sm text-gray-400">
          <div>updated at {sm.createdDate?.toString()}</div>
          <div>created at {sm.updatedDate?.toString()}</div>
        </div>
        <RenderStudyMaterialDocuments studyMaterial={sm} session={session} />
        {/* TODO content comments component */}
        <div className="flex flex-row justify-between mt-4 items-center">
          {/* TODO add comment count to dto */}
          <ContentCommentsInfo commentCount={7} contentId={sm.id!} />
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
