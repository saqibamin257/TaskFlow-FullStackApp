import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateUser } from "../services/userService";
import type { UserDTO } from "../types/user";

export const useUpdateUser = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: updateUser,

    onMutate: async (updatedUser) => {
      await queryClient.cancelQueries({ queryKey: ["users"] });

      const previousUsers = queryClient.getQueryData<UserDTO[]>(["users"]);

      //  Optimistically update
      queryClient.setQueryData<UserDTO[]>(["users"], (old) =>
        old?.map((u) =>
          u.id === updatedUser.id ? { ...u, ...updatedUser } : u,
        ),
      );

      return { previousUsers };
    },

    onError: (_err, _user, context) => {
      queryClient.setQueryData(["users"], context?.previousUsers);
    },

    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
    },
  });
};
