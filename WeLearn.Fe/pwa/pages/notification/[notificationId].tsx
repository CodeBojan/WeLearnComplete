import { AppPageWithLayout } from "../_app";
import { GetNotificationDto } from "../../types/api";
import { MdNotifications } from "react-icons/md";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import router from "next/router";
import { useState } from "react";

const Notification: AppPageWithLayout = () => {
  const [notification, setNotification] = useState<GetNotificationDto | null>(
    null
  );
  const { notificationId } = router.query;

  // TODO get notification from API
  return (
    <TitledPageContainer icon={<MdNotifications />} title={"test"}>
      <></>
    </TitledPageContainer>
  );
};

Notification.getLayout = defaultGetLayout;

export default Notification;
