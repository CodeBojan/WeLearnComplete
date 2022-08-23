import { FaGraduationCap, FaUniversity } from "react-icons/fa";
import {
  MdAcUnit,
  MdAlternateEmail,
  MdDashboard,
  MdVerifiedUser,
} from "react-icons/md";
import { ReactNode, useContext } from "react";

import { AiFillHeart } from "react-icons/ai";
import { GetAccountDto } from "../../types/api";
import { HiOutlineIdentification } from "react-icons/hi";
import IconButton from "../atoms/icon-button";
import { IoMdPerson } from "react-icons/io";
import Link from "next/link";
import { MeContext } from "../../store/me-store";
import { useAppSession } from "../../util/auth";

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
    <Link href={href}>
      <a>
        <IconButton className="flex flex-row items-center justify-start gap-x-4 text-xl text-primary-dark bg-sky-100 hover:bg-primary-dark hover:text-white rounded-lg pl-4 pr-8 py-1 text-start w-full shadow-sm">
          {icon}
          <span>{text}</span>
        </IconButton>
      </a>
    </Link>
  );
}

export function RenderPersonalInfoSection({
  text,
  icon,
}: {
  text: string;
  icon: ReactNode;
}) {
  return (
    <div className="flex flex-row items-center gap-x-2">
      {icon}
      {text}
    </div>
  );
}

export function RenderPersonalInfo({
  account,
  className,
}: {
  account: GetAccountDto | null | undefined;
  className?: string | undefined;
}) {
  return (
    <div className={`flex flex-col gap-y-2 ${className}`}>
      {account?.username && (
        <RenderPersonalInfoSection
          text={account?.username}
          icon={<MdVerifiedUser />}
        />
      )}

      {account?.email && (
        <RenderPersonalInfoSection
          text={account?.email}
          icon={<MdAlternateEmail />}
        />
      )}
      {(account?.firstName || account?.lastName) && (
        <RenderPersonalInfoSection
          text={account?.firstName + " " + account?.lastName}
          icon={<HiOutlineIdentification />}
        />
      )}
      {account?.facultyStudentId && (
        <RenderPersonalInfoSection
          text={account?.facultyStudentId}
          icon={<FaGraduationCap />}
        />
      )}
    </div>
  );
}

export default function RightSideBar() {
  const meContext = useContext(MeContext);
  const account = meContext.state.account;

  return (
    <div className="h-screen hidden md:flex flex-col p-4">
      <div className="w-64 h-screen mt-20"></div>
      <div className="fixed top-36 w-64 md:flex flex-col gap-y-4">
        <RenderPersonalInfo
          account={account}
          className="rounded-lg shadow-md p-8 text-primary-dark"
        />
        <div className="rounded-lg shadow-md p-8 flex flex-col gap-y-4">
          <RenderSideBarLink href="/" text="Dashboard" icon={<MdDashboard />} />
          <RenderSideBarLink
            href={"/courses" + "?mine=true"}
            text="My Courses"
            icon={<AiFillHeart />}
          />
        </div>
      </div>
    </div>
  );
}
