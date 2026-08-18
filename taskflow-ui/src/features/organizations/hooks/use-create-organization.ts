"use client";

import { useMutation, useQueryClient } from "@tanstack/react-query";

import { organizationService } from "../api/organization.service";
import { organizationKeys } from "./organization.keys";

import type {
  CreateOrganizationRequest,
  CreateOrganizationResponse,
} from "../types/organization.types";

export function useCreateOrganization() {
  const queryClient = useQueryClient();

  return useMutation<CreateOrganizationResponse, Error, CreateOrganizationRequest>({
    mutationFn: organizationService.createOrganization,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: organizationKeys.list(),
      });
    },
  });
}
