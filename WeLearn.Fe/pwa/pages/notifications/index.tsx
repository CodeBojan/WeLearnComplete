import { AppPageWithLayout } from "../_app";
import { GetNotificationDto } from "../../types/api";
import NotificationBell from "../../components/atoms/notification-bell";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";
import { useState } from "react";

const Notifications: AppPageWithLayout = () => {
  const [notifications, setNotifications] = useState<GetNotificationDto[]>([]);

  return (
    <TitledPageContainer
      icon={<NotificationBell theme="black" textsize="large" />}
      title={"Notifications"}
    >
      <></>
    </TitledPageContainer>
  );
};

Notifications.getLayout = defaultGetLayout;

export default Notifications;
