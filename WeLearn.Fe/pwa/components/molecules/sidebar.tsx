import { useEffect, useState } from "react";

import { AiOutlineClose } from "react-icons/ai";
import IconButton from "../atoms/icon-button";
import SidebarMenu from "./sidebar-menu";
import SignOutButton from "../auth/sign-out-button";

export interface SidebarProps {
  isOpen: boolean;
  onTryClose?: () => void;
}

export default function Sidebar({ isOpen, onTryClose }: SidebarProps) {
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
              <div className="flex flex-row justify-end">
                <IconButton 
                className="mr-4 mt-4 text-4xl"
                onClick={() => onTryClose && onTryClose()}>
                  <AiOutlineClose />
                </IconButton>
              </div>
              <div>
                <SidebarMenu />
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
