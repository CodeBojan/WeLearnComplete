import { ReactNode } from "react";

export default function FavoritesContainer({
  children,
  className,
}: {
  children: ReactNode;
  className?: string;
}) {
  return (
    <div className={`w-full flex flex-col justify-start gap-y-4 ${className}`}>
      {children}
    </div>
  );
}
