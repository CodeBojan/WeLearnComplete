import {
  AccountSelectorAction,
  AccountSelectorActionKind,
  AccountSelectorState,
} from "../../store/account-selector-store";
import { Dispatch, ReactNode } from "react";

import { GetAccountDto } from "../../types/api";
import InfiniteScroll from "react-infinite-scroller";
import Modal from "./modal";

export default function AccountSelectorModal({
  title,
  accountSelectorState,
  accountSelectorDispatch,
  accounts,
  actionButtons,
  body,
}: {
  title: ReactNode;
  accounts: GetAccountDto[];
  accountSelectorState: AccountSelectorState;
  accountSelectorDispatch: Dispatch<AccountSelectorAction>;
  actionButtons?: ReactNode | undefined;
  body: ReactNode;
}) {
  return (
    <>
      <div className=""></div>
      <Modal
        open={accountSelectorState.isOpen}
        body={body}
        footer={<></>}
        header={<>{title}</>}
        onTryClose={() =>
          accountSelectorDispatch({
            type: AccountSelectorActionKind.CLOSE_MODAL,
          })
        }
      />
    </>
  );
}
