import { AppPageWithLayout } from "../_app";
import NotificationBell from "../../components/atoms/notification-bell";
import TitledPageContainer from "../../components/containers/titled-page-container";
import { defaultGetLayout } from "../../layouts/layout";

const Notifications: AppPageWithLayout = () => {
  return (
    //
    <TitledPageContainer icon={<NotificationBell />} title={"Notifications"}>
      <></>
    </TitledPageContainer>
  );
};

Notifications.getLayout = defaultGetLayout;

export default Notifications;
