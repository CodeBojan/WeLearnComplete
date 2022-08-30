import { GetDocumentDto, GetStudyMaterialDto } from "../../types/api";

import { AppSession } from "../../types/auth";
import { DefaultExtensionType } from "react-file-icon";
import { RenderDocument } from "./render-document";

export type DocumentContainerType = {
  documentCount?: number | undefined;
  documents?: GetDocumentDto[] | undefined | null;
};

export function DocumentContainer({
  documentContainer: dc,
  session,
}: {
  documentContainer: DocumentContainerType;
  session: AppSession;
}) {
  return (
    <div>
      <div>Contains {dc.documentCount} documents</div>
      <div>
        {dc.documents?.map((document, documentIndex) => {
          const fileExtension = document.fileExtension?.replace(
            ".",
            ""
          ) as DefaultExtensionType;
          return (
            <RenderDocument
              key={document.id}
              document={document}
              fileExtension={fileExtension}
              session={session}
            />
          );
        })}
      </div>
    </div>
  );
}
