import { GetAccountDto, PutAccountDto } from "../../types/api";
import { MeContext, MeInvalidationContext } from "../../store/me-store";
import { apiAccountsMe, apiMethodFetcher } from "../../util/api";
import { useContext, useEffect, useState } from "react";

import { AppPageWithLayout } from "../_app";
import Button from "../../components/atoms/button";
import Input from "../../components/atoms/input";
import { defaultGetLayout } from "../../layouts/layout";
import { toast } from "react-toastify";
import { useAppSession } from "../../util/auth";

const renderInput = (
  label: string,
  text: string | null | undefined,
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void,
  props?: any
) => {
  const value = text ?? "";
  return <Input label={label} text={text} onChange={onChange} props={props} />;
};

const Settings: AppPageWithLayout = () => {
  const { data: session } = useAppSession();
  const { state: meContext, dispatch: meDispatch } = useContext(MeContext);
  const { meInvalidate } = useContext(MeInvalidationContext);

  const account = meContext.account;

  const [apiError, setApiError] = useState<string | null>(null);
  const [firstName, setFirstName] = useState(account?.firstName ?? "");
  const [lastName, setLastName] = useState(account?.lastName ?? "");
  const [facultyStudentId, setFacultyStudentId] = useState(
    account?.facultyStudentId ?? ""
  );

  useEffect(() => {
    if (account) {
      account.firstName && setFirstName(account.firstName);
      account.lastName && setLastName(account.lastName);
      account.facultyStudentId && setFacultyStudentId(account.facultyStudentId);
    }
  }, [meContext]);

  if (!session || !account) return <></>;

  return (
    <div className="grow flex flex-col w-full items-start justify-start mt-20 px-8">
      <div>
        <h1 className="text-3xl font-bold text-left">Settings</h1>
      </div>
      <div className="flex flex-col gap-y-2 mt-8 w-full">
        {renderInput("User Id", account.id)}
        {renderInput("Username", account.username)}
        {renderInput("Email", account.email)}
        {renderInput("First Name", firstName, (e) => {
          setFirstName(e.target.value);
        })}
        {renderInput("Last Name", lastName, (e) => {
          setLastName(e.target.value);
        })}
        {renderInput("Faculty/Student Id", facultyStudentId, (e) => {
          setFacultyStudentId(e.target.value);
        })}
        <Button
          onClick={(e) => {
            apiMethodFetcher(apiAccountsMe, session.accessToken, "PUT", {
              firstName: firstName,
              lastName: lastName,
              facultyStudentId: facultyStudentId,
            } as PutAccountDto)
              .then((res) => {
                const dto = res as GetAccountDto;
                meInvalidate();
                setApiError(null);
                toast("Settings updated successfully", { type: "success" });
              })
              .catch((err) => {
                toast(`Failed To Update Profile: ${err}`, { type: "error" });
                setApiError(err.message);
              });
          }}
          className="mt-8 text-xl"
        >
          Save
        </Button>
      </div>
    </div>
  );
};

Settings.getLayout = defaultGetLayout;

export default Settings;
