import { MdAlternateEmail, MdVerifiedUser } from "react-icons/md";
import {
  RenderPersonalInfo,
  RenderPersonalInfoSection,
} from "./right-side-bar";

import { AiOutlineClose } from "react-icons/ai";
import { GetAccountDto } from "../../types/api";
import { HiOutlineIdentification } from "react-icons/hi";
import IconButton from "../atoms/icon-button";
import { MeContext } from "../../store/me-store";
import SidebarMenu from "./sidebar-menu";
import heroBackgroundSvg from "../../public/abstract-timekeeper.svg";
import { url } from "inspector";
import { useContext } from "react";

export interface SidebarProps {
  isOpen: boolean;
  onTryClose?: () => void;
}

export default function Sidebar({ isOpen, onTryClose }: SidebarProps) {
  const meContext = useContext(MeContext);
  const account = meContext.state.account;

  return (
    <>
      <aside
        className={`
        flex flex-row
        transform top-0 left-0 w-full fixed h-full overflow-auto ease-in-out transition-all duration-300 z-30 ${
          isOpen ? "translate-x-0" : "-translate-x-full"
        }`}
      >
        <div className="bg-white h-full w-10/12 md:w-6/12 lg:w-5/12 xl:w-3/12">
          <div className="flex flex-col h-full justify-between">
            <div>
              <div
                className="bg-cover"
                style={{ backgroundImage: "url('/vanishing-stripes.svg')" }}
              >
                <div className="flex flex-row justify-end">
                  <IconButton
                    className="mr-4 mt-4 text-4xl text-white"
                    onClick={() => onTryClose && onTryClose()}
                  >
                    <AiOutlineClose />
                  </IconButton>
                </div>
                {account && getAccountInfo(account)}
              </div>
              <div>
                <SidebarMenu onNavigate={() => onTryClose && onTryClose()} />
              </div>
            </div>
          </div>
        </div>
        <div
          className={`grow bg-black cursor-pointer transition-opacity ease-in-out duration-1000 opacity-25`}
          onClick={() => onTryClose && onTryClose()}
        ></div>
      </aside>
    </>
  );
}

function getAccountInfo(account: GetAccountDto) {
  return (
    <div className="flex flex-col items-start justify-start gap-y-2 p-8">
      <RenderPersonalInfo className="text-white" account={account} />
    </div>
  );
}
