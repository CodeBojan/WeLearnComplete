import { ReactNode } from "react";

export default function FavoriteInfoContainer({
  children,
}: {
  children: ReactNode;
}) {
  return <div className="flex flex-row items-center gap-x-4">{children}</div>;
}
