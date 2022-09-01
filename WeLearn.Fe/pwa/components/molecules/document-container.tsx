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
  if (dc.documentCount === 0 || !dc.documents) return <></>;

  return (
    <div>
      <div>
        Contains {dc.documentCount}{" "}
        {dc.documentCount == 1 ? "document" : "documents"}{" "}
      </div>
      <div className="flex flex-row gap-4 flex-wrap mt-4">
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
