export function InputLabel({ label }: { label: string }) {
  return (
    <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300">
      {label}
    </label>
  );
}

export default function Input({
  label,
  text,
  onChange,
  placeholder,
  props,
}: {
  label: string;
  text: string | null | undefined;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
  placeholder?: string;
  props?: any;
}) {
  const value = text ?? "";
  return (
    <div className="grow">
      <InputLabel label={label} />
      <input
        className={`bg-gray-100 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-gray-400 dark:focus:ring-blue-500 dark:focus:border-blue-500 disabled:bg-gray-300 ${
          !onChange ? "cursor-not-allowed" : ""
        }`}
        value={value}
        readOnly={!onChange}
        disabled={!onChange}
        onChange={onChange}
        placeholder={placeholder}
        {...props}
      />
    </div>
  );
}
