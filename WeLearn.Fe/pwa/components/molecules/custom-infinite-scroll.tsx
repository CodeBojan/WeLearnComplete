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
  dataLength: number | null | undefined;
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
        (dataLength === null || dataLength === undefined || hasMore) && (
          <div key={0} className="flex flex-col w-full gap-y-8">
            {[...Array(5)].map((_, index) => (
              <div
                key={index}
                className="border border-blue-100 shadow rounded-md p-4 w-full"
              >
                <div className="animate-pulse flex space-x-4">
                  <div className="rounded-full bg-slate-200 h-10 w-10"></div>
                  <div className="flex-1 space-y-6 py-1">
                    <div className="h-10 bg-slate-200 rounded"></div>
                    <div className="space-y-3">
                      <div className="grid grid-cols-3 gap-4">
                        <div className="h-6 bg-slate-200 rounded col-span-2"></div>
                        <div className="h-6 bg-slate-200 rounded col-span-1"></div>
                      </div>
                      <div className="h-6 bg-slate-200 rounded"></div>
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )
      }
      scrollableTarget={scrollableTarget}
      endMessage={(dataLength == 0 || !hasMore) && <EndMessage />}
    >
      {children}
    </InfiniteScroll>
  );
}
