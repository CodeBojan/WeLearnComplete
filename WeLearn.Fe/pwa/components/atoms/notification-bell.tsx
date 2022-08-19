import { AiOutlineBell, AiOutlineLogout, AiOutlineMenu } from "react-icons/ai";

import IconButton from "./icon-button";

export default function NotificationBell() {
  return (
    <IconButton className="relative items-center">
      <AiOutlineBell className="text-2xl text-white" />
      <div className="inline-flex absolute top-2 -right-3.5 justify-center items-center w-6 h-6 text-xs font-bold text-white bg-red-500 rounded-full border-2 border-primary-dark dark:border-gray-900">
        20
      </div>
    </IconButton>
  );
}
