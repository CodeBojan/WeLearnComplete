import { ReactNode } from "react";

export default function FavoritesContainer({
  children,
}: {
  children: ReactNode;
}) {
  return (
    <div className="w-full flex flex-col justify-start mt-8 gap-y-8">
      {children}
    </div>
  );
}
