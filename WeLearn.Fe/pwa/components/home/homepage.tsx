import { signIn, useSession } from "next-auth/react";

import Link from "next/link";
import { signOutUrl } from "../../util/auth";
import styles from "../../styles/HomePage.module.scss";

export default function HomePage() {
    const { data: session, status } = useSession();
    
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

            {!session ? (
              <a
                className={styles.card + " text-3xl font-bold"}
                onClick={(e) => {
                  e.preventDefault();
                  signIn("identityServer");
                }}
              >
                Sign In
              </a>
            ) : (
              <>
                <p>{session.user?.email}</p>
                <Link href={signOutUrl}>
                  <a className={styles.card + " text-3xl font-bold"}>
                    Sign Out
                  </a>
                </Link>
                <Link href="/metric-schema">
                  <a className={styles.card + " text-3xl font-bold"}>
                    Metric Schema
                  </a>
                </Link>
              </>
            )}
          </div>
        </main>

        <footer className={styles.footer}>
          <a
            href="https://vercel.com?utm_source=create-next-app&utm_medium=default-template&utm_campaign=create-next-app"
            target="_blank"
            rel="noopener noreferrer"
          >
            Powered by{" "}
            <img src="/vercel.svg" alt="Vercel Logo" className={styles.logo} />
          </a>
        </footer>
      </div>
    </>
  );
}
