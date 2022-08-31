import { W, w } from "windstitch";

import { ComponentProps } from "../../types/components";
import React from "react";

const StyledButton = w.button(
  "cursor-pointer rounded-lg py-2 text-center font-semibold",
  {
    variants: {
      variant: {
        normal: "text-white bg-primary hover:bg-primary-dark border disabled:bg-blue-200 disabled:cursor-default active:bg-primary",
        outline:
          "text-primary-dark bg-white hover:bg-white hover:border-primary-dark border",
        danger:
          "text-white bg-red-600 hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 disabled:bg-red-200 disabled:cursor-default",
      },
      padding: {
        normal: "px-2",
        large: "px-4",
      },
    },
    defaultVariants: {
      variant: "normal",
      padding: "normal",
    },
  }
);

// TODO add active:...

type StyledButtonProps = W.Infer<typeof StyledButton>;
export declare type ButtonProps = StyledButtonProps & ComponentProps;

export default function Button({ children, onClick, ...props }: ButtonProps) {
  return (
    <StyledButton
      {...props}
      onClick={(e: React.MouseEvent<HTMLButtonElement, MouseEvent>) =>
        onClick && onClick(e)
      }
    >
      {children}
    </StyledButton>
  );
}
