import { MeActionKind, initialMeState, meReducer } from "../store/me-store";
import { ReactElement, useEffect, useReducer, useState } from "react";

import { AppPageWithLayout } from "./_app";
import BottomNav from "../components/molecules/bottom-nav";
import { GetAccountDto } from "../types/api";
import Layout from "../layouts/layout";
import Link from "next/link";
import Navbar from "../components/molecules/navbar";
import Sidebar from "../components/molecules/sidebar";
import SignOutButton from "../components/auth/sign-out-button";
import { apiGetFetcher } from "../util/api";
import styles from "../styles/Home.module.scss";
import { useAppSession } from "../util/auth";
import useSWR from "swr";

const Home: AppPageWithLayout = () => {
  const { data: session, status } = useAppSession();

  return (
    <>
      <div className={styles.container}>
        <main className={styles.main}>
          <h1 className={styles.title}>
            Welcome to <a href="https://nextjs.org">Next.js!</a>
          </h1>
          <p className={styles.description}>
            Get started by editing{" "}
            <code className={styles.code}>pages/index.js</code>
          </p>

          <div className={styles.grid}>
            <a href="https://nextjs.org/docs" className={styles.card}>
              <h3>Documentation &rarr;</h3>
              <p>Find in-depth information about Next.js features and API.</p>
            </a>

            <a href="https://nextjs.org/learn" className={styles.card}>
              <h3>Learn &rarr;</h3>
              <p>Learn about Next.js in an interactive course with quizzes!</p>
            </a>

            <a
              href="https://github.com/vercel/next.js/tree/canary/examples"
              className={styles.card}
            >
              <h3>Examples &rarr;</h3>
              <p>Discover and deploy boilerplate example Next.js projects.</p>
            </a>

            <a
              href="https://vercel.com/new?utm_source=create-next-app&utm_medium=default-template&utm_campaign=create-next-app"
              className={styles.card}
            >
              <h3>Deploy &rarr;</h3>
              <p>
                Instantly deploy your Next.js site to a public URL with Vercel.
              </p>
            </a>

            <>
              <p>{session.user?.email}</p>
              <SignOutButton className={styles.card + " text-3xl font-bold"} />

              <Link href="/protected">
                <a className={styles.card + " text-3xl font-bold"}>Protected</a>
              </Link>
              <button
                className={styles.card + "text-3xl font-bold"}
                onClick={() =>
                  navigator.clipboard.writeText(session.accessToken)
                }
              >
                Access Token
              </button>
              <button
                className={styles.card + " text-3xl font-bold"}
                onClick={() => {}}
              >
                Get Feed
              </button>
            </>
          </div>
        </main>
      </div>
    </>
  );
};

Home.getLayout = function getLayout(page: ReactElement) {
  return <Layout>{page}</Layout>;
};

export default Home;
