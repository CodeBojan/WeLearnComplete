import { FunctionComponentElement, ReactNode, cloneElement } from "react";

import { AiFillHeart } from "react-icons/ai";
import { ComponentProps } from "../../types/components";
import { MdDashboard } from "react-icons/md";
import NotificationBell from "../atoms/notification-bell";

const bottomNavButton = (
  text: string,
  icon: FunctionComponentElement<{ class: string }>
) => {
  return (
    <div
      className="flex flex-col 
items-center
justify-center cursor-pointer p-2 hover:bg-blue-400 hover:bg-opacity-50 hover:rounded-lg"
    >
      {cloneElement(icon, { class: icon.props.class + " text-2xl" })}
      <span>{text}</span>
    </div>
  );
};

export default function BottomNav() {
  return (
    <>
      <div className="md:hidden bottom-0 flex flex-col fixed w-full items-center justify-evenly px-6 h-20 text-white border-t border-gray-200 z-10 bg-primary-dark text-md">
        <div className="flex w-full items-center justify-evenly px-6">
          {bottomNavButton("Dashboard", <MdDashboard />)}
          {bottomNavButton("Notifications", <NotificationBell />)}
          {bottomNavButton("My Courses", <AiFillHeart />)}
        </div>
      </div>
    </>
  );
}
