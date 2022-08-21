import { Key } from "react";

export default function FavoritableContainer({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="grow flex flex-row text-xl font-semibold items-center justify-between hover:bg-slate-500 hover:bg-opacity-20 hover:cursor-pointer py-4 px-4 rounded
    border-l-4 border-r-2 shadow-md border-slate-200">
      {children}
    </div>
  );
}
