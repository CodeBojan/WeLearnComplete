import { ReactElement, useContext } from "react";

import { AppPageWithLayout } from "../_app";
import Layout from "../../layouts/layout";
import { MeContext } from "../../store/me-store";

const Settings: AppPageWithLayout = () => {
  const meContext = useContext(MeContext);
  const account = meContext.account;

  return (
    <div className="min-h-screen flex flex-col items-center justify-center">
      <div className="grow flex flex-col w-full items-start justify-start mt-20 pl-8">
        <div>
          <h1 className="text-3xl font-bold text-left">Settings</h1>
        </div>
        <div className="flex flex-col gap-y-2">
          <div>
            <span className="text-2xl font-semibold mr-8">User Id</span>
            <input value={meContext.account?.id}></input>
          </div>
        </div>
      </div>
    </div>
  );
};

Settings.getLayout = function getLayout(page: ReactElement) {
  return <Layout>{page}</Layout>;
};

export default Settings;
