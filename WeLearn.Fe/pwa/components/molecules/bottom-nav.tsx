import { ComponentProps } from "../../types/components";

export default function BottomNav() {
  return (
    <>
      <div className="md:hidden bottom-0 flex flex-col fixed w-full items-center justify-evenly px-6 h-16 bg-white text-gray-700 border-t border-gray-200 z-10">
        <div className="flex w-full items-center justify-evenly px-6">
          <div>b1</div>
          <div>b2</div>
          <div>b3</div>
        </div>
      </div>
    </>
  );
}
