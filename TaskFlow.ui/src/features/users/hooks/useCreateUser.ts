import { createUser } from "../services/userService";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export const useCreateUser = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: createUser,
    onSuccess: () => {
      // Refresh users list
      queryClient.invalidateQueries({ queryKey: ["users"] });
    },
  });
};
