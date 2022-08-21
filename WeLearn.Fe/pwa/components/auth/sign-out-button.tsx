import Link from "next/link";
import { signOutUrl } from "../../util/auth";

export default function SignOutButton({ ...props }) {
  return (
    <Link href={signOutUrl}>
      <button {...props}>Sign Out</button>
    </Link>
  );
}
