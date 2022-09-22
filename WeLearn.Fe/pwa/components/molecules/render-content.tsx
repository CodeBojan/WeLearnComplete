import CreatedUpdatedDates, { RenderDate } from "./created-updated-dates";
import {
  MdCalendarToday,
  MdEditNote,
  MdShare,
  MdSubject,
} from "react-icons/md";
import parse, {
  DOMNode,
  Element,
  HTMLReactParserOptions,
  attributesToProps,
  domToReact,
} from "html-react-parser";

import { AiFillNotification } from "react-icons/ai";
import { AppSession } from "../../types/auth";
import { BiLinkExternal } from "react-icons/bi";
import ContentCommentsInfo from "./content-comments-info";
import { DocumentContainer } from "./document-container";
import { GetContentDto } from "../../types/api";
import Link from "next/link";
import { ReactNode } from "react";
import styles from "../../styles/RenderContent.module.scss";
import { useRouter } from "next/router";

export default function RenderContent({
  content: c,
  session,
}: {
  content: GetContentDto;
  session: AppSession;
}) {
  const options: HTMLReactParserOptions = {
    replace: (domNode: DOMNode) => {
      if (domNode instanceof Element) {
        const props = attributesToProps(domNode.attribs);
        if (domNode.tagName === "img" && domNode?.attribs.height) {
          props.height = "";
          props.width = "";
          const imgSrc = props.src;
          if (c.externalSystem?.url && imgSrc?.startsWith("/"))
            props.src = `${c.externalSystem.url}${imgSrc}`;
          props.class = "external-image";
        }

        if (domNode.tagName === "a" && props.href) {
          props.href = "";
          domNode.tagName = "span";
        }

        if (
          domNode.tagName === "div" &&
          domNode.attribs.class === "attachmentsContainer"
        ) {
          return <></>;
        }

        domNode.attribs = props;
      }

      return domNode;
    },
  };
  const { locale } = useRouter();

  return (
    <div className="flex flex-col gap-y-2 rounded-lg shadow-md p-4 md:p-8">
      <div className="flex flex-row items-center justify-between">
        <div className="flex flex-row items-center gap-x-4">
          <img
            className="w-8 rounded-lg"
            src={c.externalSystem?.logoUrl ?? "/logo.svg"}
          />
          <div className="flex flex-col">
            {c.externalSystem && (
              <span className="font-bold">
                {c.externalSystem?.friendlyName}
              </span>
            )}
            {c.author && <span className="">{c.author}</span>}
            {c.externalCreatedDate && (
              <RenderDate date={c.externalCreatedDate} locale={locale} />
            )}
          </div>
        </div>
        <div className="flex flex-row items-center gap-x-4">
          {RenderCourseInfo(c)}
          {RenderStudyYearInfo(c)}
          <div className="text-2xl">
            <RenderContentTypeIcon c={c} />
          </div>
        </div>
      </div>
      <span className="text-lg font-bold">{c.title}</span>
      <div className="break-all">
        {c.body?.startsWith("<") ? parse(c.body, options) : c.body}
        <div className="flex flex-row gap-x-2">
          <span>
            <span className="font-semibold">{c.creator?.username}</span>
            {c.creator?.facultyStudentId && (
              <span className="text-sm"> ({c.creator.facultyStudentId})</span>
            )}
          </span>
        </div>
      </div>
      <div className="flex flex-col gap-y-1 text-sm text-gray-400">
        <CreatedUpdatedDates
          entity={c}
          locale={locale}
          createdAsImported={true}
        />
      </div>
      <DocumentContainer
        documentContainer={{
          documentCount: c.documentCount,
          documents: c.documents,
        }}
        session={session}
      />
      <div className="flex flex-row justify-between mt-4 items-center">
        <ContentCommentsInfo contentId={c.id!} commentCount={c.commentCount} />
        <div className="flex gap-x-4 items-center">
          <MdShare className="text-2xl" />
          {c.externalUrl && (
            <div className="hover:bg-gray-200 p-1 rounded-lg">
              <Link href={c.externalUrl}>
                <a>
                  <BiLinkExternal className="text-2xl" />
                </a>
              </Link>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

function RenderCourseInfo(c: GetContentDto): ReactNode {
  return (
    c.course && (
      <div className="hover:bg-gray-200 p-1 rounded-lg">
        <Link href={`/course/${c.course.id}`}>
          <a>
            <div className="flex gap-x-2 items-center">
              <span className="font-semibold">{c.course.shortName}</span>
              <span className="text-sm">({c.course?.code})</span>
            </div>
          </a>
        </Link>
      </div>
    )
  );
}

function RenderStudyYearInfo(c: GetContentDto) {
  return (
    c.studyYear && (
      <div className="hover:bg-gray-200 p-1 rounded-lg">
        <Link href={`study-year/${c.studyYear.id}`}>
          <a>
            <div className="flex gap-x-2 items-center">
              <span className="font-semibold">{c.studyYear.shortName}</span>
            </div>
          </a>
        </Link>
      </div>
    )
  );
}

function RenderContentTypeIcon({ c }: { c: GetContentDto }) {
  switch (c.type) {
    case "Post":
    case "NoticeCourse":
      return <MdSubject />;
    case "NoticeStudyYear":
      return <MdCalendarToday />;
    case "NoticeGeneral":
      return <AiFillNotification />;
    case "StudyMaterial":
      return <MdEditNote />;
    default:
      return <></>;
  }
}
