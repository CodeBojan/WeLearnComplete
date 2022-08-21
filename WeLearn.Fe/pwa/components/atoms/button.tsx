import { W, w } from "windstitch";

import { ComponentProps } from "../../types/components";
import React from "react";

const StyledButton = w.button(
  "cursor-pointer rounded-lg py-2 text-center font-semibold",
  {
    variants: {
      variant: {
        normal: "text-white bg-primary hover:bg-primary-dark border",
        outline:
          "text-primary-dark bg-white hover:bg-white hover:border-primary-dark border",
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
