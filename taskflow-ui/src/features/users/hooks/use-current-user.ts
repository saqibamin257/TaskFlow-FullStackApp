import { useQuery } from "@tanstack/react-query";
import { userService } from "../api/user.service";

export function useCurrentUser() {
  return useQuery({
    queryKey: ["current-user"],
    queryFn: () => userService.getCurrentUser(),
    staleTime: 5 * 60 * 1000, // 5 mins
  });
}
