import { ImCross } from "react-icons/im";
import { ReactNode } from "react";

export default function Modal({
  open,
  header,
  body,
  footer,
  onTryClose,
  showFooter = true,
}: {
  open: boolean;
  header: ReactNode;
  body: ReactNode;
  footer: ReactNode;
  onTryClose?: () => void;
  showFooter?: boolean;
}) {
  return !open ? (
    <div className="hidden"></div>
  ) : (
    <div
      tabIndex={-1}
      className="overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 w-full md:inset-0 h-modal h-full justify-center items-center flex bg-black bg-opacity-25"
      aria-modal="true"
      role="dialog"
    >
      <div className="relative p-4 w-full max-w-2xl h-full md:h-auto">
        <div className="relative bg-white rounded-lg shadow dark:bg-gray-700">
          <div className="flex justify-between items-start p-4 rounded-t border-b dark:border-gray-600">
            <h3 className="text-xl font-semibold ">{header}</h3>
            <button
              onClick={() => onTryClose && onTryClose()}
              type="button"
              className="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center dark:hover:bg-gray-600 dark:hover:text-white"
              data-modal-toggle="defaultModal"
            >
              <ImCross />
              <span className="sr-only">Close modal</span>
            </button>
          </div>
          {body}
          {/* TODO remove comments */}
          {/* <div className="p-6 space-y-6">
            <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
              With less than a month to go before the European Union enacts new
              consumer privacy laws for its citizens, companies around the world
              are updating their terms of service agreements to comply.
            </p>
            <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
              The European Union’s General Data Protection Regulation (G.D.P.R.)
              goes into effect on May 25 and is meant to ensure a common set of
              data rights in the European Union. It requires organizations to
              notify users as soon as possible of high-risk data breaches that
              could personally affect them.
            </p>
          </div> */}
          {/* footer */}
          {showFooter && (
            <div className="flex items-center p-4 space-x-2 rounded-b border-t border-gray-200 dark:border-gray-600">
              {footer}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
