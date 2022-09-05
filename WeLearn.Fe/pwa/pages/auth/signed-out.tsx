import { useEffect, useState } from "react";

import router from "next/router";
import { useSession } from "next-auth/react";

export default function SignedOutPage() {
  const { data: session, status } = useSession();
  const loading = status === "loading";
  const [redirectSeconds, setRedirectSeconds] = useState(3);

  useEffect(() => {
    if (redirectSeconds == 0) {
      router.push("/");
      return;
    }

    setTimeout(() => {
      setRedirectSeconds((redirectSeconds) => redirectSeconds - 1);
    }, 1000);
  }, [redirectSeconds]);

  return (
    <>
      <div className="h-screen flex flex-col gap-y-4 justify-center items-center">
        <img className="max-w-xl" src="/logo.svg" />
        <p className="text-8xl font-bold">WeLearn</p>
        <h1 className="text-2xl font-bold">Successfully signed out.</h1>
        <h2 className="text-xl font-semibold">
          You will be redirected to the home page in {redirectSeconds}s.
        </h2>
      </div>
    </>
  );
}
