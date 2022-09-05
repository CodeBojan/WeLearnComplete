import EndMessage from "../atoms/end-message";
import InfiniteScroll from "react-infinite-scroll-component";

export default function CustomInfiniteScroll({
  dataLength,
  next,
  hasMore,
  scrollableTarget,
  children,
  showLoader,
}: {
  dataLength: number | undefined;
  next: () => void;
  hasMore: boolean;
  children: React.ReactNode;
  scrollableTarget?: string | null | undefined;
  showLoader?: boolean | undefined;
}) {
  return (
    <InfiniteScroll
      dataLength={dataLength ?? 0}
      next={next}
      hasMore={hasMore}
      loader={
        (!showLoader ? hasMore : showLoader) && <div key={0}>Loading...</div>
      }
      scrollableTarget={scrollableTarget}
      endMessage={!hasMore && <EndMessage />}
    >
      {children}
    </InfiniteScroll>
  );
}
