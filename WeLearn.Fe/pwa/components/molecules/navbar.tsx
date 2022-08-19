import { AiOutlineBell, AiOutlineLogout, AiOutlineMenu } from "react-icons/ai";

import IconButton from "../atoms/icon-button";
import NotificationBell from "../atoms/notification-bell";

export interface NavbarProps {
  onDrawerToggle?: () => void;
}

export default function Navbar({ onDrawerToggle }: NavbarProps) {
  return (
    <>
      <nav className="flex fixed w-full items-center justify-between px-6 h-16 bg-primary-dark border-b border-gray-200 z-10 text-white">
        <div>
          <IconButton onClick={() => onDrawerToggle && onDrawerToggle()}>
            <AiOutlineMenu className="text-white text-2xl" />
          </IconButton>
        </div>
        <div>
          <div className="flex flex-row items-center gap-x-4">
            <div className="hidden md:block mr-4">
              <NotificationBell />
            </div>
            <span className="font-semibold text-lg text-white">WeLearn</span>
          </div>
        </div>
      </nav>
    </>
  );
}
