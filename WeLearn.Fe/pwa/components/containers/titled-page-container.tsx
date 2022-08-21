import { ReactNode } from "react";
import StyledPageContainer from "./page-container";

export default function TitledPageContainer({
  icon,
  title,
  children,
  skeleton,
}: {
  icon: ReactNode;
  title: ReactNode;
  children: ReactNode;
  skeleton?: ReactNode | undefined;
}) {
  return (
    <StyledPageContainer>
      <div className="w-full flex flex-row gap-x-8 items-center text-4xl font-bold pb-2 border-b-2">
        <div>{icon}</div>
        <div className="w-full flex items-center">
          {title ? (
            <div>{title}</div>
          ) : !skeleton ? (
            <div className="w-full flex items-center animate-pulse bg-gray-300 rounded-lg text-gray-400">
              Loading...
            </div>
          ) : (
            skeleton
          )}
        </div>
      </div>
      {children}
    </StyledPageContainer>
  );
}
