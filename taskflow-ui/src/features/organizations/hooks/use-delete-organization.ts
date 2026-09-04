"use client";

import { useMutation, useQueryClient } from "@tanstack/react-query";
import { organizationService } from "../api/organization.service";
import { organizationKeys } from "./organization.keys";

export function useDeleteOrganization() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => organizationService.deleteOrganization(id),

    onSuccess: (_, id) => {
      queryClient.invalidateQueries({
        queryKey: organizationKeys.all,
      });

      queryClient.removeQueries({
        queryKey: organizationKeys.detail(id),
      });
    },
  });
}
