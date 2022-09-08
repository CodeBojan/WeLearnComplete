import { DefaultExtensionType, FileIcon, defaultStyles } from "react-file-icon";
import { apiDocument, apiMethodFetcher, apiRoute } from "../../util/api";

import { AppSession } from "../../types/auth";
import { GetDocumentDto } from "../../types/api";
import Link from "next/link";
import fileDownload from "js-file-download";
import { toast } from "react-toastify";

export function RenderDocument({
  document,
  fileExtension,
  session,
}: {
  document: GetDocumentDto;
  fileExtension: DefaultExtensionType;
  session: AppSession;
}): JSX.Element {
  return (
    <div key={document.id}>
      <div className="">
        <div className="">
          <div className="hover:drop-shadow-xl hover:cursor-pointer">
            <div
              onClick={(e) => {
                apiMethodFetcher(
                  apiDocument(document.id!),
                  session.accessToken,
                  "GET",
                  undefined,
                  false
                )
                  .then((res) => {
                    return res.blob() as Promise<Blob>;
                  })
                  .then((blob) => {
                    const splitFile = document.fileName?.split(".");
                    const downloadFileName =
                      document.fileExtension?.replace(".", "") ===
                      splitFile?.pop()
                        ? document.fileName
                        : `${document.fileName}.${document.fileExtension}`;
                    try {
                      fileDownload(blob, downloadFileName!);
                      toast(`Downloaded ${document.fileName} successfully`, {
                        type: "success",
                      });
                    } catch (error) {
                      toast(`Failed to download file: ${error}`, {
                        type: "error",
                      });
                    }
                  });
              }}
            >
              <div className="flex flex-row items-center gap-x-4">
                <div className="w-12">
                  <FileIcon
                    extension={fileExtension}
                    {...defaultStyles[fileExtension]}
                  />
                </div>
                <div>{document.fileName}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
