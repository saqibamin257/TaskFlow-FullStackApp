"use client";

import { useMutation, useQueryClient } from "@tanstack/react-query";

import { organizationService } from "../api/organization.service";
import { organizationKeys } from "./organization.keys";

export function useCreateOrganization() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: organizationService.createOrganization,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: organizationKeys.list(),
      });
    },
  });
}