import { useQuery } from "@tanstack/react-query";
import { getUsers } from "../services/userService";
import type { UserDTO } from "../types/user";

export const useUsers = () => {
  return useQuery<UserDTO[], Error>({
    queryKey: ["users"],
    queryFn: getUsers,

    // Production configs
    staleTime: 1000 * 60, // 1 minute (cache freshness)
    retry: 2, // retry failed requests
    refetchOnWindowFocus: false, // avoid unnecessary refetch
  });
};
