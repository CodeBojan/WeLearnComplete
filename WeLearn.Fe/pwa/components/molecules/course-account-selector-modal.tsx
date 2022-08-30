import {
  AccountSelectorAction,
  AccountSelectorState,
} from "../../store/account-selector-store";
import { Dispatch, ReactNode } from "react";

import AccountSelectorModal from "./account-selector-modal";
import InfiniteScroll from "react-infinite-scroller";
import { RenderPersonalInfo } from "./right-side-bar";
import useCourseAccounts from "../../util/useCourseAccounts";
import useStudyYearAccounts from "../../util/useStudyYearAccounts";

export default function CourseAccountSelectorModal({
  courseId,
  accountSelectorState,
  accountSelectorDispatch,
  actionButtons,
}: {
  courseId: string;
  accountSelectorState: AccountSelectorState;
  accountSelectorDispatch: Dispatch<AccountSelectorAction>;
  actionButtons?: ReactNode | undefined;
}) {
  const { studyYearAccounts, size, setSize, isLoadingMore, isReachingEnd } =
    useCourseAccounts({ courseId });
  // TODO skeletonize
  if (!studyYearAccounts) return <></>;

  return (
    <AccountSelectorModal
      title={"Course Accounts"}
      accountSelectorState={accountSelectorState}
      accountSelectorDispatch={accountSelectorDispatch}
      accounts={studyYearAccounts}
      actionButtons={actionButtons}
      body={
        <InfiniteScroll
          pageStart={1}
          loadMore={() => {
            setSize(size + 1);
          }}
          hasMore={!isReachingEnd && !isLoadingMore}
          loader={<div key={0}>Loading...</div>}
        >
          <div className="flex flex-col items-start m-4">
            {studyYearAccounts?.map((account, index) => (
              <div
                key={account.id}
                className="flex flex-row items-center justify-between gap-x-4 w-full"
              >
                <div className="w-full flex flex-row items-center justify-between py-4 border-b-2">
                  <RenderPersonalInfo account={account} />
                  {actionButtons && <div>{actionButtons} </div>}
                </div>
              </div>
            ))}
          </div>
        </InfiniteScroll>
      }
    />
  );
}
