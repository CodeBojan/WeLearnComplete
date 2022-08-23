import { AiFillHeart } from "react-icons/ai";
import IconButton from "../atoms/icon-button";
import Link from "next/link";
import { MdDashboard } from "react-icons/md";
import { ReactNode } from "react";

function RenderSideBarLink({
  text,
  href,
  icon,
}: {
  text: string;
  href: string;
  icon: ReactNode;
}) {
  return (
    <div>
      <IconButton className="flex flex-row items-center justify-start gap-x-4 text-xl text-primary-dark bg-sky-100 hover:bg-primary-dark hover:text-white rounded-lg pl-4 pr-8 py-1 text-start w-full shadow-sm">
        {icon}
        <Link href={href}>
          <a>{text}</a>
        </Link>
      </IconButton>
    </div>
  );
}

export default function RightSideBar() {
  // TODO adjust - add links that were in the bottom nav, plus some more
  return (
    <div className="h-screen hidden md:flex flex-col">
      <div className="w-64 h-screen mt-20">
        {/* TODO add user info also in fixed */}
      </div>
      <div className="fixed top-36 right-0 hidden w-64 rounded-lg shadow-md p-8 m-4 md:flex flex-col gap-y-4">
        <RenderSideBarLink href="/" text="Dashboard" icon={<MdDashboard />} />
        <RenderSideBarLink
          href={"/courses" + "?mine=true"}
          text="My Courses"
          icon={<AiFillHeart />}
        />
      </div>
    </div>
  );
}
