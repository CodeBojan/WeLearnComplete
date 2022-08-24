import {
  CourseMaterialUploadRequestAction,
  CourseMaterialUploadRequestActionKind,
  CourseMaterialUploadRequestState,
} from "../../store/course-material-upload-request-store";
import { Dispatch, SetStateAction, useEffect, useRef } from "react";
import Input, { InputLabel } from "../atoms/input";

import { AiOutlineCloudUpload } from "react-icons/ai";
import Button from "../atoms/button";
import { FileUploader } from "react-drag-drop-files";
import { ImCross } from "react-icons/im";
import Modal from "./modal";
import { toast } from "react-toastify";

export default function CourseMaterialUploadRequestModal({
  uploadModalOpen,
  materialUploadState,
  materialUploadDispatch,
  removeFile,
  addFile,
  upload,
  setUploadModalOpen,
  clearModalState,
  clearFiles,
}: {
  uploadModalOpen: boolean;
  materialUploadState: CourseMaterialUploadRequestState;
  materialUploadDispatch: Dispatch<CourseMaterialUploadRequestAction>;
  removeFile: (file: File) => void;
  addFile: (file: File) => void;
  upload: () => void;
  setUploadModalOpen: Dispatch<SetStateAction<boolean>>;
  clearModalState: () => void;
  clearFiles: () => void;
}) {
  function usePrevious(value: File[]) {
    const ref = useRef<File[]>();
    useEffect(() => {
      ref.current = value;
    }, [value]);
    return ref.current;
  }

  const previous = usePrevious(materialUploadState.files);

  useEffect(() => {
    const previousLength = previous?.length ?? 0;
    const diff = materialUploadState.files.length - previousLength;
    if (diff <= 0) return;

    const newFiles = materialUploadState.files.slice(
      previousLength,
      materialUploadState.files.length
    );
    newFiles.forEach((file) => {
      toast(`Added file ${file.name}`, { type: "success" });
    });
  }, [materialUploadState.files]);

  return (
    <Modal
      open={uploadModalOpen}
      header="Upload Course Material"
      body={
        <div className="m-8 flex flex-col gap-y-4">
          <Input
            label="Description"
            text={materialUploadState.body}
            placeholder="2022 Solved Mock Exam"
            onChange={(e) => {
              materialUploadDispatch({
                type: CourseMaterialUploadRequestActionKind.SET_BODY,
                body: e.target.value,
              });
            }}
          ></Input>
          <Input
            label="Remark"
            text={materialUploadState.remark}
            placeholder="Please review my solution :)"
            onChange={(e) => {
              materialUploadDispatch({
                type: CourseMaterialUploadRequestActionKind.SET_REMARK,
                remark: e.target.value,
              });
            }}
          ></Input>

          <div className="flex flex-col w-full">
            <div className="flex flex-col mb-4">
              <InputLabel label="Files" />
              {materialUploadState.files &&
                materialUploadState.files.length > 0 && (
                  <div className="flex flex-row gap-4 flex-wrap">
                    {materialUploadState.files.map((file, index) =>
                      renderModalFile(index, file, () => removeFile(file))
                    )}
                  </div>
                )}
            </div>
            <div className="flex justify-center items-center w-full">
              <FileUploader
                multiple={true}
                classes="w-full"
                handleChange={(files: File | FileList) => {
                  if (files instanceof File) {
                    addFile(files);
                  } else
                    Array.from(files).forEach((file) => {
                      addFile(file);
                    });
                }}
              >
                <label className="flex flex-col justify-center items-center w-full h-full bg-gray-50 rounded-lg border-2 border-gray-300 border-dashed cursor-pointer dark:hover:bg-bray-800 dark:bg-gray-700 hover:bg-gray-100 dark:border-gray-600 dark:hover:border-gray-500 dark:hover:bg-gray-600">
                  <div className="flex flex-col justify-center items-center pt-5 pb-6">
                    <AiOutlineCloudUpload className="text-4xl text-gray-500" />
                    <p className="mb-2 text-sm text-gray-500 dark:text-gray-400">
                      <span className="font-semibold">Click to upload</span> or
                      drag and drop
                    </p>
                    <p className="text-xs text-gray-500 dark:text-gray-400">
                      DOCX, PDF, PNG, JPG ... (MAX. 3.0MB / FILE)
                    </p>
                  </div>
                </label>
              </FileUploader>
            </div>
            <div className="mt-4">
              <Button variant="outline" onClick={() => clearFiles()}>
                Clear Files
              </Button>
            </div>
          </div>
        </div>
      }
      footer={
        <div className="mx-4 flex gap-x-4">
          <Button onClick={() => upload()}>Upload</Button>
          <Button
            variant="danger"
            onClick={() => {
              setUploadModalOpen(false);
              clearModalState();
            }}
          >
            Cancel
          </Button>
        </div>
      }
      onTryClose={() => setUploadModalOpen(false)}
    />
  );
}

function renderModalFile(
  index: number,
  file: File,
  onRemove?: () => void
): JSX.Element {
  // TODO add document version selector
  return (
    <div
      key={index}
      className="flex flex-row items-center gap-x-2 bg-orange-500 text-white rounded-lg p-2 hover:bg-orange-900 cursor-pointer"
    >
      {file.name}
      <ImCross onClick={() => onRemove && onRemove()} />
    </div>
  );
}
