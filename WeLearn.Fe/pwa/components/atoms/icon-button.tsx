import { ComponentProps } from "../../types/components";
import { ReactElement } from "react";

export type IconButtonProps = ComponentProps & {
  onClick?: () => void;
  className?: string | undefined;
};

export default function IconButton({
  onClick,
  children,
  ...props
}: IconButtonProps) {
  return (
    <button onClick={() => onClick && onClick()} {...props}>
      {children}
    </button>
  );
}
