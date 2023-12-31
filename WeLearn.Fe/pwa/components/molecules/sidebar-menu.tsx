import {
  DetailedReactHTMLElement,
  FunctionComponentElement,
  ReactNode,
  cloneElement,
} from "react";
import { MdCalendarToday, MdSubject } from "react-icons/md";

import { AiTwotoneSetting } from "react-icons/ai";
import Link from "next/link";
import SignOutButton from "../auth/sign-out-button";

export interface SidebarMenuProps {
  onNavigate?: () => void;
}

const renderLink = (
  href: string,
  text: string,
  icon: FunctionComponentElement<{ className: string }>,
  onNavigate?: () => void
) => (
  <Link href={href}>
    <a
      key={href}
      className="shadow-sm text-lg font-semibold hover:bg-slate-500 hover:bg-opacity-50 py-4 cursor-pointer"
      onClick={() => onNavigate && onNavigate()}
    >
      <div className="flex flex-row items-center px-8">
        <div>
          {cloneElement(icon, {
            className: icon.props.className + " text-2xl mr-8",
          })}
        </div>
        <span className="text-2xl">{text}</span>
      </div>
    </a>
  </Link>
);

export default function SidebarMenu({ onNavigate }: SidebarMenuProps) {
  return (
    <>
      <div className="flex flex-col gap-y-2">
        {renderLink(
          "/study-years",
          "Study Years",
          <MdCalendarToday />,
          onNavigate
        )}
        {renderLink("/courses", "Courses", <MdSubject />, onNavigate)}
        {renderLink("/settings", "Settings", <AiTwotoneSetting />, onNavigate)}
        <div className="flex items-center justify-center mx-8 mt-4">
          <SignOutButton className="bg-red-600 hover:bg-red-700 rounded-lg text-white grow md:grow-0 md:w-10/12 lg:w-5/12 py-2" />
        </div>
      </div>
    </>
  );
}
