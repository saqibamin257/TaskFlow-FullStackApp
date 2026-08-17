"use client";
import { useQuery } from "@tanstack/react-query";
import { organizationService } from "../api/organization.service";
import { organizationKeys } from "./organization.keys";

export function useOrganization(id: string) {
  return useQuery({
    queryKey: organizationKeys.detail(id),
    queryFn: () => organizationService.getOrganizationById(id),
    enabled: !!id,
  });
}
