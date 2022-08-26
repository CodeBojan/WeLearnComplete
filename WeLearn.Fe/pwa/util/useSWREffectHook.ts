import { Dispatch, SetStateAction, useEffect } from "react";
import { Entity, PageDtos } from "./api";

export function useSWREffectHook<TEntity extends Entity>(
  pageDtos: PageDtos<TEntity>[] | undefined,
  setStateAction: Dispatch<SetStateAction<TEntity[] | null | undefined>>
): void {
  useEffect(() => {
    const entityMap = new Map<string, TEntity>();
    pageDtos?.forEach((pageDto) => {
      pageDto.data?.forEach((entity) => {
        entityMap.set(entity.id!, entity);
      });
    });

    setStateAction(Array.from(entityMap.values()));
  }, [pageDtos]);
}
