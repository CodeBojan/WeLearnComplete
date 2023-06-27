import {
  FunctionComponentElement,
  ReactNode,
  cloneElement,
  useContext,
} from "react";

import { AiFillHeart } from "react-icons/ai";
import { ComponentProps } from "../../types/components";
import Link from "next/link";
import { MdDashboard } from "react-icons/md";
import NotificationBell from "../atoms/notification-bell";
import { NotificationsContext } from "../../store/notifications-store";

const bottomNavButton = (
  text: string,
  icon: FunctionComponentElement<{ className: string }>,
  href: string
) => {
  return (
    <Link href={href}>
      <a>
        <div
          className="flex flex-col 
    items-center
    justify-center cursor-pointer p-2 hover:bg-blue-400 hover:bg-opacity-50 hover:rounded-lg"
        >
          {cloneElement(icon, {
            className: icon.props.className + " text-2xl",
          })}
          <span>{text}</span>
        </div>
      </a>
    </Link>
  );
};

export default function BottomNav() {
  const notificationsContext = useContext(NotificationsContext);

  return (
    <>
      <div className="md:hidden bottom-0 flex flex-col fixed w-full items-center justify-evenly h-18 text-white z-10 bg-primary-dark text-md">
        <div className="flex w-full items-center justify-evenly">
          {bottomNavButton("Dashboard", <MdDashboard />, "/")}
          {bottomNavButton(
            "Notifications",
            <NotificationBell
              notifCount={notificationsContext.state.unreadCount}
            />,
            "/notifications"
          )}
          {bottomNavButton(
            "My Courses",
            <AiFillHeart />,
            "/courses" + "?mine=true"
          )}
        </div>
      </div>
    </>
  );
}
