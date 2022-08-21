import { Key } from "react";

export default function FavoritableContainer({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="grow flex flex-row text-xl font-semibold items-center justify-between hover:bg-slate-500 hover:bg-opacity-20 hover:cursor-pointer py-2 px-4 rounded">
      {children}
    </div>
  );
}
