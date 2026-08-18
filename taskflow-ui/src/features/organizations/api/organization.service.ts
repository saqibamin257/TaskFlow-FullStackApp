import api from "@/lib/axios";
import type {
  Organization,
  CreateOrganizationRequest,
  CreateOrganizationResponse,
  UpdateOrganizationRequest,
} from "../types/organization.types";

export const organizationService = {
  // Get all organizations
  async getOrganizations(): Promise<Organization[]> {
    const response = await api.get<Organization[]>("/organization");
    return response.data;
  },

  //Get Single Organization
  async getOrganizationById(id: string): Promise<Organization> {
    console.log("get-Organization");
    const response = await api.get<Organization>(`/organization/${id}`);
    return response.data;
  },

  // Create a new Organization
  async createOrganization(
    request: CreateOrganizationRequest,
  ): Promise<CreateOrganizationResponse> {
    const response = await api.post<CreateOrganizationResponse>(
      "/organization",
      request,
    );
    return response.data;
  },

  // Update Organization
  async updateOrganization(
    request: UpdateOrganizationRequest,
  ): Promise<Organization> {
    const response = await api.put<Organization>(
      `/organization/${request.id}`,
      request,
    );
    return response.data;
  },

  // Delete an organization
  async deleteOrganization(id: string): Promise<void> {
    await api.delete(`/organization/${id}`);
  },
};
