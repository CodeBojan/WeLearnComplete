import { AiOutlineMenu } from "react-icons/ai";
import { BsSearch } from "react-icons/bs";
import IconButton from "../atoms/icon-button";
import Link from "next/link";
import NotificationBell from "../atoms/notification-bell";
import { NotificationsContext } from "../../store/notifications-store";
import router from "next/router";
import { useContext } from "react";

export interface NavbarProps {
  onDrawerToggle?: () => void;
}

export default function Navbar({ onDrawerToggle }: NavbarProps) {
  const notificationsContext = useContext(NotificationsContext);

  return (
    <>
      <nav className="flex fixed w-full items-center justify-between px-6 h-16 bg-primary-dark border-b border-gray-200 z-10 text-white">
        <IconButton onClick={() => onDrawerToggle && onDrawerToggle()}>
          <AiOutlineMenu className="text-white text-2xl" />
        </IconButton>
        <div>
          <div className="flex flex-row gap-x-4 items-center">
            <input
              className="p-2 rounded-lg placeholder:text-white text-white bg-blue-800 "
              placeholder="Search ..."
            ></input>
            <BsSearch />
          </div>
        </div>
        <div>
          <div className="flex flex-row items-center gap-x-4">
            <div className="hidden md:block mr-4">
              <Link href="/notifications">
                <a>
                  <NotificationBell
                    notifCount={notificationsContext.state.unreadCount}
                  />
                </a>
              </Link>
            </div>
            <span className="hidden md:block font-semibold text-lg text-white">WeLearn</span>
          </div>
        </div>
      </nav>
    </>
  );
}
