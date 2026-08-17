export interface Organization {
  id: string;
  name: string;
  slug: string;
  description: string;
  logoUrl: string | null;
  ownerUserId: string;
  isActive: boolean;
  createdAtUTC: string;
  updatedAtUTC: string | null;
}
export interface CreateOrganizationResponse {
  id: string;
  name: string;
  slug: string;
  description: string;
  logoUrl: string | null;
  ownerUserId: string;
  isActive: boolean;
  createdAtUTC: string;
}

export interface CreateOrganizationRequest {
  name: string;
  slug: string;
  description: string;
  logoUrl: string | null;
}

export interface UpdateOrganizationRequest {
  id: string;
  name: string;
  slug: string;
  description: string;
  logoUrl: string | null;
}
