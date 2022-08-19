import Button from "../atoms/button";
import { signInUtil } from "../../util/auth";

export default function SignInButton({ ...props }) {
  return (
    <Button
      {...props}
      onClick={(e) => {
        e.preventDefault();
        signInUtil();
      }}
    >
      Sign In
    </Button>
  );
}
