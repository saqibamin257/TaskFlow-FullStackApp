import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteUser } from "../services/userService";
import type { UserDTO } from "../types/user";

export const useDeleteUser = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteUser,

    onMutate: async (id) => {
      await queryClient.cancelQueries({ queryKey: ["users"] });

      const previousUsers = queryClient.getQueryData<UserDTO[]>(["users"]);

      // Optimistic remove
      queryClient.setQueryData<UserDTO[]>(["users"], (old) =>
        old?.filter((u) => u.id !== id),
      );

      return { previousUsers };
    },

    onError: (_err, _id, context) => {
      queryClient.setQueryData(["users"], context?.previousUsers);
    },

    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
    },
  });
};
