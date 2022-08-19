import Link from "next/link";
import SignOutButton from "../auth/sign-out-button";

const renderLink = (href: string, text: string) => (
  <div key={href} className="shadow-sm px-8 text-lg font-semibold">
    <Link href={href}>{text}</Link>
  </div>
);

export default function SidebarMenu() {
  return (
    <>
      <div className="flex flex-col gap-y-2">
        {renderLink("/study-years", "Study Years")}
        {renderLink("/courses", "Courses")}
        <SignOutButton />
      </div>
    </>
  );
}
