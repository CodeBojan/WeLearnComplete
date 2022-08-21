import { W, w } from "windstitch";

import { AiOutlineBell } from "react-icons/ai";
import IconButton from "./icon-button";

const BellIcon = w(AiOutlineBell, {
  variants: {
    theme: {
      light: "text-white",
      dark: "text-primary-dark",
      black: "text-black",
    },
    textsize: {
      medium: "text-2xl",
      large: "text-4xl",
    },
  },
  defaultVariants: {
    theme: "light",
    textsize: "medium",
  },
});

type NotificationBellProps = W.Infer<typeof BellIcon>;

export default function NotificationBell({
  notifCount,
  ...props
}: NotificationBellProps & { notifCount?: number }) {
  const theme = props.theme || "light";
  const textSize = props.textsize || "medium";
  const _notifsCount = notifCount || 0;
  return (
    <div className="relative items-center">
      <BellIcon theme={theme} textsize={textSize} />
      {_notifsCount > 0 && (
        <div className="inline-flex absolute top-2 -right-3.5 justify-center items-center w-6 h-6 text-xs font-bold text-white bg-red-500 rounded-full border-2 border-primary-dark dark:border-gray-900">
          {_notifsCount}
        </div>
      )}
    </div>
  );
}
