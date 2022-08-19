import {
  DetailedReactHTMLElement,
  FunctionComponentElement,
  ReactNode,
  cloneElement,
} from "react";
import { MdCalendarToday, MdSubject } from "react-icons/md";

import Link from "next/link";
import SignOutButton from "../auth/sign-out-button";

const renderLink = (
  href: string,
  text: string,
  icon: FunctionComponentElement<{ class: string }>
) => (
  <div key={href} className="shadow-sm text-lg font-semibold hover:bg-slate-500 hover:bg-opacity-50 py-4 cursor-pointer">
    <Link href={href}>
      <div className="flex flex-row items-center px-8">
        <div>
          {cloneElement(icon, { class: icon.props.class + " text-2xl mr-8" })}
        </div>
        <span className="text-2xl">{text}</span>
      </div>
    </Link>
  </div>
);

export default function SidebarMenu() {
  return (
    <>
      <div className="flex flex-col gap-y-2">
        {renderLink("/study-years", "Study Years", <MdCalendarToday />)}
        {renderLink("/courses", "Courses", <MdSubject />)}
        <div className="flex items-center justify-center mx-8 mt-4">
          <SignOutButton className="bg-red-600 text-white grow md:grow-0 md:w-10/12 lg:w-5/12 py-2 text-center" />
        </div>
      </div>
    </>
  );
}
