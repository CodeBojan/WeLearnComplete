import { ComponentProps } from "../types/components";
import LoadingAuth from "../components/auth/loading-auth";
import { useAppSession } from "../util/auth";

export interface LayoutProps extends ComponentProps {}

export default function Layout({ children, ...props }: LayoutProps) {
  const { data: session, status } = useAppSession();

  return (
    <div className="layout">
      {!session ? (
        <LoadingAuth></LoadingAuth>
      ) : (
        <div>
          {/* Move things from index page to this layout */}
          <div>{children}</div>
        </div>
      )}
    </div>
  );
}
