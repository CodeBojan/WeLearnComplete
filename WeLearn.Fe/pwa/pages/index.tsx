import { ReactElement, useState } from "react";

import { AppPageWithLayout } from "./_app";
import CustomInfiniteScroll from "../components/molecules/custom-infinite-scroll";
import { FaEyeDropper } from "react-icons/fa";
import { GetContentDto } from "../types/api";
import Layout from "../layouts/layout";
import Link from "next/link";
import { MdDashboard } from "react-icons/md";
import RenderContent from "../components/molecules/render-content";
import SignOutButton from "../components/auth/sign-out-button";
import TitledPageContainer from "../components/containers/titled-page-container";
import styles from "../styles/Home.module.scss";
import { useAppSession } from "../util/auth";
import useFeed from "../util/useFeed";

const Home: AppPageWithLayout = () => {
  const { data: session, status } = useAppSession();
  const { feed, size, setSize, isLoadingMore, isReachingEnd, mutate, hasMore } =
    useFeed();

  return (
    <TitledPageContainer icon={<MdDashboard />} title={"Dashboard"}>
      <div className="my-8 w-full">
        <CustomInfiniteScroll
          dataLength={feed?.length}
          next={() => setSize(size + 1)}
          hasMore={hasMore}
        >
          <div className="flex flex-col gap-y-4">
            {feed?.map((content, index) => (
              <RenderContent
                content={content}
                session={session}
                key={content.id}
              />
            ))}
          </div>
        </CustomInfiniteScroll>
      </div>
    </TitledPageContainer>
  );
};

Home.getLayout = function getLayout(page: ReactElement) {
  return <Layout>{page}</Layout>;
};

export default Home;
