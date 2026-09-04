import api from "@/lib/axios";
import type {
  Organization,
  CreateOrganizationRequest,
  CreateOrganizationResponse,
  UpdateOrganizationRequest,
} from "../types/organization.types";

export const organizationService = {
  // Get all organizations, getOrganizations()
  async getOrganizations(): Promise<Organization[]> {
    const response = await api.get<Organization[]>("/organization");
    return response.data;
  },

  //Get Single Organization, getOrganization(id)
  async getOrganizationById(id: string): Promise<Organization> {
    console.log("get-Organization");
    const response = await api.get<Organization>(`/organization/${id}`);
    return response.data;
  },

  // Create a new Organization, createOrganization(request)
  async createOrganization(
    request: CreateOrganizationRequest,
  ): Promise<CreateOrganizationResponse> {
    const response = await api.post<CreateOrganizationResponse>(
      "/organization",
      request,
    );
    return response.data;
  },

  // Update Organization, updateOrganization(id, request)
  async updateOrganization(
    id: string,
    request: UpdateOrganizationRequest,
  ): Promise<Organization> {
    const response = await api.put<Organization>(
      `/organization/${id}`,
      request,
    );
    return response.data;
  },

  // Delete an organization, deleteOrganization(id)
  async deleteOrganization(id: string): Promise<void> {
    await api.delete(`/organization/${id}`);
  },
};
