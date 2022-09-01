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

  return (
    <div className="flex flex-col gap-y-2 rounded-lg shadow-md p-8">
      <div className="flex flex-row items-center justify-between">
        <div className="flex flex-row items-center gap-x-4">
          {c.externalSystem?.logoUrl && (
            <img className="w-8 rounded-lg" src={c.externalSystem?.logoUrl} />
          )}
          <div className="flex flex-col">
            <span className="font-bold">{c.externalSystem?.friendlyName}</span>
            <span className="">{c.author}</span>
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
      <div className="text-lg font-semibold">{c.title}</div>
      <div className="">
        {c.body?.startsWith("<") ? parse(c.body, options) : c.body}
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
        <div className="flex gap-x-4">
          <MdShare className="text-2xl" />
          {c.externalUrl && (
            <Link href={c.externalUrl}>
              <a>
                <BiLinkExternal className="text-2xl" />
              </a>
            </Link>
          )}
        </div>
      </div>
    </div>
  );
}

function RenderCourseInfo(c: GetContentDto): ReactNode {
  return (
    c.course && (
      <>
        <Link href={`/course/${c.course.id}`}>
          <a>
            <div className="flex gap-x-2 items-center">
              <span className="font-semibold">{c.course.shortName}</span>
              <span className="text-sm">({c.course?.code})</span>
            </div>
          </a>
        </Link>
      </>
    )
  );
}

function RenderStudyYearInfo(c: GetContentDto) {
  // throw new Error("Function not implemented.");
  return (
    c.studyYear && (
      <>
        <Link href={`study-year/${c.studyYear.id}`}>
          <a>
            <div className="flex gap-x-2 items-center">
              <span className="font-semibold">{c.studyYear.shortName}</span>
            </div>
          </a>
        </Link>
      </>
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
