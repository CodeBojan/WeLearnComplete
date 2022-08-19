import { ComponentProps } from "../../types/components";
import React from "react";
import styled from "styled-components";

export declare interface ButtonProps extends ComponentProps {
  onClick?: (e: React.MouseEvent<HTMLButtonElement>) => void | undefined;
  className?: string | undefined;
}

export default function Button({ children, onClick, ...props }: ButtonProps) {
  return (
    <button
      {...props}
      onClick={(e) => onClick && onClick(e)}
      className={`cursor-pointer text-white bg-primary rounded py-2 text-center ${props?.className}`}
    >
      {children}
    </button>
  );
}
