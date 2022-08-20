export interface InputProps {
  className?: string;
  value?: string;
  props?: any;
}

export default function Input({ className, value, ...props }: InputProps) {
  return (
    <input
      className={` ${className ? className : ""}`}
      value={value ?? ""}
      {...props}
    />
  );
}
