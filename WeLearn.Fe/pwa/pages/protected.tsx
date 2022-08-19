import { signIn, useSession } from "next-auth/react";
import { useEffect, useState } from "react";

import AccessDenied from "../components/access-denied";
import { AppPageWithLayout } from "./_app";
import Layout from "../layouts/layout";
import { signInUtil } from "../util/auth";

const ProtectedPage: AppPageWithLayout = () => {
  const { data: session, status } = useSession();
  const loading = status === "loading";
  const [content, setContent] = useState();

  // Fetch content from protected route
  useEffect(() => {
    if (session?.error === "RefreshAccessTokenError") {
      signIn(); // TODO
    }

    const fetchData = async () => {
      const res = await fetch("/api/examples/protected");
      const json = await res.json();
      if (json.content) {
        setContent(json.content);
      }
    };
    fetchData();
  }, [session]);

  // When rendering client side don't display anything until loading is complete
  if (typeof window !== "undefined" && loading) return null;

  // If no session exists, display access denied message
  if (!session) {
    return <AccessDenied />;
  }

  // If session exists, display content
  return (
    <>
      {/* <input type={"button"} onClick={() => signInUtil()}>
        Re-sign in
      </input> */}
      <input></input>
      <h1>Protected Page</h1>
      <p>{JSON.stringify(session)}</p>
      <p>
        <strong>{content ?? "\u00a0"}</strong>
      </p>
    </>
  );
};

ProtectedPage.getLayout = (page) => <Layout>{page}</Layout>;

export default ProtectedPage;
