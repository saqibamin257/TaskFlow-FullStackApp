import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createUser } from "../services/userService";
import type { UserDTO } from "../types/user";

export const useCreateUser = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createUser,

    // OPTIMISTIC UPDATE START
    // onMutate : UI updates instantly.
    onMutate: async (newUser) => {
      // Cancel ongoing fetches
      await queryClient.cancelQueries({ queryKey: ["users"] });

      // Snapshot previous data
      const previousUsers = queryClient.getQueryData<UserDTO[]>(["users"]);

      const tempId = Date.now();
      console.log("Temporary ID:", tempId);
      // Optimistically update cache
      queryClient.setQueryData<UserDTO[]>(["users"], (old) => [
        ...(old || []),
        {
          id: tempId, // temporary ID
          ...newUser,
        },
      ]);

      return { previousUsers, tempId };
    },

    // If API fails → rollback
    onError: (err, newUser, context) => {
      console.error("Error:", err);
      console.log("Failed user:", newUser);
      queryClient.setQueryData(["users"], context?.previousUsers);
    },

    //  AFTER SUCCESS
    onSuccess: (data, _variables, context) => {
      console.log("🟢 Actual ID from backend:", data.id);
      console.log("🟡 Previously used temp ID:", context?.tempId);
    },

    // After success or error → sync with server
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
    },
  });
};
