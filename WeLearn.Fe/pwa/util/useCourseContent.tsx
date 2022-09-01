import { GetContentDto } from "../types/api";
import { apiCourseContent } from "./api";
import { usePagedData } from "./usePagedData";
import { useState } from "react";

export default function useCourseContent({ courseId }: { courseId: string }) {
  const [pageSize, setPageSize] = useState(5); // TODO page size
  const { entities, size, setSize, isLoadingMore, isReachingEnd, mutate } =
    usePagedData<GetContentDto>({
      pageSize,
      url: apiCourseContent(courseId),
    });

  return {
    content: entities,
    size,
    setSize,
    isLoadingMore,
    isReachingEnd,
    mutate,
  };
}
