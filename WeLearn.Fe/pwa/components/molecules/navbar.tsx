import { AiOutlineBell, AiOutlineLogout, AiOutlineMenu } from "react-icons/ai";

import IconButton from "../atoms/icon-button";

export interface NavbarProps {
  onDrawerToggle?: () => void;
}

export default function Navbar({ onDrawerToggle }: NavbarProps) {
  return (
    <>
      <nav className="flex fixed w-full items-center justify-between px-6 h-16 bg-white text-gray-700 border-b border-gray-200 z-10">
        <div>
          <IconButton onClick={() => onDrawerToggle && onDrawerToggle()}>
            <AiOutlineMenu />
          </IconButton>
        </div>
        <div>
          <div className="flex flex-row items-center gap-x-4">
            <div className="mr-4">
              <IconButton className="relative items-center">
                <AiOutlineBell className="text-2xl" />
                <div className="inline-flex absolute top-2 -right-4 justify-center items-center w-6 h-6 text-xs font-bold text-white bg-red-500 rounded-full border-2 border-white dark:border-gray-900">
                  20
                </div>
              </IconButton>
            </div>
            <span className="font-semibold text-lg">WeLearn</span>
          </div>
        </div>
      </nav>
    </>
  );
}
