import EndMessage from "../atoms/end-message";
import InfiniteScroll from "react-infinite-scroll-component";

export default function CustomInfiniteScroll({
  dataLength,
  next,
  hasMore,
  scrollableTarget,
  children,
}: {
  dataLength: number | undefined;
  next: () => void;
  hasMore: boolean;
  children: React.ReactNode;
  scrollableTarget?: string | null | undefined;
}) {
  return (
    <InfiniteScroll
      dataLength={dataLength ?? 0}
      next={next}
      hasMore={hasMore}
      loader={hasMore && <div key={0}>Loading...</div>}
      scrollableTarget={scrollableTarget}
      endMessage={!hasMore && <EndMessage />}
    >
      {children}
    </InfiniteScroll>
  );
}
