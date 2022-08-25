import Button from "./button";

export default function OnlyMineButton({
  onlyMine,
  onClick,
}: {
  onlyMine: boolean;
  onClick: () => void;
}) {
  return (
    <Button
      variant={onlyMine ? "normal" : "outline"}
      padding="large"
      onClick={() => onClick && onClick()}
    >
      Only Mine
    </Button>
  );
}
