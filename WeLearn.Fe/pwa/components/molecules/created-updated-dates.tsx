import TimeAgo from "timeago-react";
import Tippy from "@tippyjs/react";
import { useTranslation } from "next-i18next";

export default function CreatedUpdatedDates({
  entity,
  locale,
}: {
  entity: { createdDate?: Date | undefined; updatedDate?: Date | undefined };
  className?: string;
  locale?: string;
}) {
  const { t } = useTranslation("common");

  return (
    <div>
      {entity.updatedDate != entity.createdDate && (
        <div className="flex flex-row gap-x-1">
          {t("updated")}
          <RenderDate date={entity.updatedDate} locale={locale} />
        </div>
      )}
      <div className="flex flex-row gap-x-1">
        {t("created")}
        <RenderDate date={entity.createdDate} locale={locale} />
      </div>
    </div>
  );
}
export function RenderDate({
  date,
  locale,
}: {
  date: Date | undefined;
  locale: string | undefined;
}) {
  const { t } = useTranslation("common");

  return (
    <Tippy placement="right" content={t("dateTime", { date })}>
      <div className="underline decoration-sky-500">
        <TimeAgo datetime={date!} locale={locale} />
      </div>
    </Tippy>
  );
}
