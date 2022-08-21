import { ReactNode } from "react";
import StyledPageContainer from "./page-container";

export default function TitledPageContainer({
  icon,
  title,
  children
}: {
  icon: ReactNode;
  title: ReactNode;
  children: ReactNode;
}) {
  return (
    <StyledPageContainer>
      <div className="flex flex-row gap-x-8 items-center text-4xl font-bold">
        <div>{icon}</div>
        <div>{title}</div>
      </div>
      {children}
    </StyledPageContainer>
  );
}
