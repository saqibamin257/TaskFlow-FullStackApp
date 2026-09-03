"use client";

import { useMutation, useQueryClient } from "@tanstack/react-query";
import { organizationService } from "../api/organization.service";
import { organizationKeys } from "../hooks/organization.keys";
import type { UpdateOrganizationRequest } from "../types/organization.types";


export function useUpdateOrganization() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      id,
      request,
    }: {
      id: string;
      request: UpdateOrganizationRequest;
    }) => organizationService.updateOrganization(id, request),

    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({
        queryKey: organizationKeys.detail(variables.id),
      });

      queryClient.invalidateQueries({
        queryKey: organizationKeys.list(),
      });
    },
  });
}