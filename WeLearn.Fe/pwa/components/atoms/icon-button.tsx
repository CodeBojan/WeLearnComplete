import { ComponentProps } from "../../types/components";
import { ReactElement } from "react";

export type IconButtonProps = ComponentProps & {
  onClick?: () => void;
  className?: string | undefined;
};

export default function IconButton({
  children,
  onClick,
  ...props
}: IconButtonProps) {
  return (
    <button onClick={() => onClick && onClick()} {...props}>
      {children}
    </button>
  );
}
