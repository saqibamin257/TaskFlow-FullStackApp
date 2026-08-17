import { z } from "zod";

export const createOrganizationSchema = z.object({
  name: z
    .string()
    .min(2, "Organization name must be at least 2 characters")
    .max(100, "Organization name cannot exceed 100 characters"),

  slug: z
    .string()
    .min(2, "Slug must be at least 2 characters")
    .max(100, "Slug cannot exceed 100 characters")
    .regex(
      /^[a-z0-9]+(?:-[a-z0-9]+)*$/,
      "Slug can only contain lowercase letters, numbers, and hyphens",
    ),

  description: z
    .string()
    .min(2, "Description is required")
    .max(500, "Description cannot exceed 500 characters"),

  logoUrl: z
    .union([z.url("Please enter a valid URL"), z.literal("")])
    .optional(),
});

export type CreateOrganizationFormData = z.infer<
  typeof createOrganizationSchema
>;
