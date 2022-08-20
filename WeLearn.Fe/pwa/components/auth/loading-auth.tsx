import { CSSProperties, useEffect, useRef, useState } from "react";
import {
  ClipLoader,
  ClockLoader,
  FadeLoader,
  HashLoader,
  MoonLoader,
} from "react-spinners";
import { getIsIssuer, signInUtil } from "../../util/auth";

import Button from "../atoms/button";
import Link from "next/link";
import SignInButton from "./sign-in-button";

const override: CSSProperties = {
  display: "block",
};

export default function LoadingAuth() {
  const [loading, setLoading] = useState(true);
  const [timeoutExpired, setTimeoutExpired] = useState(false);
  const timerRef = useRef(
    setTimeout(() => {
      setTimeoutExpired(true);
    }, 3000)
  );

  useEffect(() => {
    return () => {
      timerRef.current && clearTimeout(timerRef.current);
    };
  }, []);

  return (
    <>
      <div className="h-screen flex flex-col items-center justify-center mx-8">
        <div className="flex flex-col items-center gap-y-16 md:gap-y-24">
          <p className="text-8xl font-bold">WeLearn</p>
          <HashLoader
            color={"#3b82f6"}
            loading={loading}
            cssOverride={override}
            size={150}
          />
        </div>

        <div
          className={`grid grid-cols-1 w-full px-4 mt-12 md:mt-20 gap-y-4 ease-in-out transition-opacity duration-500 ${
            timeoutExpired ? "opacity-100" : "opacity-0 invisible"
          }`}
        >
          <p className="row-span-1 text-center">
            Don't have an account or haven't signed in?
          </p>
          <SignInButton className="row-span-1 text-2xl" />
          {/* <Link href="/offline">
            <Button className="mt-8 text-2xl bg-purple-600">Go Offline</Button>
          </Link> */}
        </div>
      </div>
    </>
  );
}
