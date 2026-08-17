"use client";
import { useQuery } from "@tanstack/react-query";
import { organizationService } from "../api/organization.service";
import { organizationKeys } from "./organization.keys";

export function useOrganizations() {
  return useQuery({
    queryKey: organizationKeys.list(),
    queryFn: organizationService.getOrganizations,
  });
}
