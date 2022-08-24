import { DefaultExtensionType, FileIcon, defaultStyles } from "react-file-icon";
import {
  GetDocumentDto,
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

import InfiniteScroll from "react-infinite-scroller";
import Link from "next/link";
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
            <RenderStudyMaterial studyMaterial={sm} />
          ))}
      </div>
    </InfiniteScroll>
  );
}

function RenderStudyMaterial({
  studyMaterial: sm,
}: {
  studyMaterial: GetStudyMaterialDto;
}) {
  return (
    <div key={sm.id}>
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
        <RenderStudyMaterialDocuments studyMaterial={sm} />
        <div className="flex flex-row justify-between mt-4 items-center">
          <div className="flex flex-row gap-x-4 items-center hover:bg-gray-200 rounded-lg p-2 cursor-pointer">
            <span className="">7</span> <MdComment className="text-2xl" />
          </div>
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
}: {
  studyMaterial: GetStudyMaterialDto;
}) {
  return (
    <div>
      {/* TODO change course material upload request to have title and body and remark, while not requiring files - the body can contain useful links */}
      {(sm.documentCount ?? 0) > 0 && sm.documents && (
        <div>
          <div>Contains {sm.documentCount} documents</div>
          <div>
            {sm.documents?.map((document, documentIndex) => {
              const fileExtension = document.fileExtension?.replace(
                ".",
                ""
              ) as DefaultExtensionType;
              return (
                <RenderDocument
                  document={document}
                  fileExtension={fileExtension}
                />
              );
            })}
          </div>
        </div>
      )}
    </div>
  );
}

function RenderDocument({
  document,
  fileExtension,
}: {
  document: GetDocumentDto;
  fileExtension: DefaultExtensionType;
}): JSX.Element {
  return (
    <div key={document.id}>
      <div className="">
        <div className="">
          <div className="hover:drop-shadow-xl">
            <Link href={document.uri || ""}>
              <a>
                <div className="flex flex-row items-center gap-x-4">
                  <div className="w-12">
                    <FileIcon
                      extension={fileExtension}
                      {...defaultStyles[fileExtension]}
                    />
                  </div>
                  <div>{document.fileName}</div>
                </div>
              </a>
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
}
