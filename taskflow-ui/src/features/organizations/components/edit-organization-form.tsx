"use client";

import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useUpdateOrganization } from "../hooks/use-update-organization";

import {
  updateOrganizationSchema,
  type UpdateOrganizationFormData,
} from "../schemas/organization-schema";


import type {
  Organization,
  UpdateOrganizationRequest,
} from "../types/organization.types";

import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Textarea } from "@/components/ui/textarea";

interface EditOrganizationFormProps {
  organization: Organization;
  onSuccess?: () => void;
}

export function EditOrganizationForm({
  organization,
  onSuccess,
}: EditOrganizationFormProps) {
  const updateOrganization = useUpdateOrganization();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<UpdateOrganizationFormData>({
    resolver: zodResolver(updateOrganizationSchema),

    defaultValues: {
      name: organization.name,
      slug: organization.slug,
      description: organization.description,
      logoUrl: organization.logoUrl ?? "",
    },
  });

  /*
   * Reset the form whenever the organization changes.
   *
   * This is useful because the same dialog component can
   * remain mounted while different organization data is loaded.
   */
  useEffect(() => {
    reset({
      name: organization.name,
      slug: organization.slug,
      description: organization.description,
      logoUrl: organization.logoUrl ?? "",
    });
  }, [organization, reset]);

  const onSubmit = (data: UpdateOrganizationFormData) => {
    const request: UpdateOrganizationRequest = {
      id: organization.id,
      name: data.name,
      slug: data.slug,
      description: data.description,
      logoUrl: data.logoUrl || null,
    };

    updateOrganization.mutate(
      {
        id: organization.id,
        request,
      },
      {
        onSuccess: () => {
          onSuccess?.();
        },
      }
    );
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-5">
      {/* Organization Name */}
      <div className="space-y-2">
        <label className="text-sm font-medium">
          Organization Name
        </label>

        <Input {...register("name")} />

        {errors.name && (
          <p className="text-sm text-destructive">
            {errors.name.message}
          </p>
        )}
      </div>

      {/* Slug */}
      <div className="space-y-2">
        <label className="text-sm font-medium">
          Slug
        </label>

        <Input {...register("slug")} />

        {errors.slug && (
          <p className="text-sm text-destructive">
            {errors.slug.message}
          </p>
        )}
      </div>

      {/* Description */}
      <div className="space-y-2">
        <label className="text-sm font-medium">
          Description
        </label>

        <Textarea
          {...register("description")}
          rows={4}
        />

        {errors.description && (
          <p className="text-sm text-destructive">
            {errors.description.message}
          </p>
        )}
      </div>

      {/* Logo URL */}
      <div className="space-y-2">
        <label className="text-sm font-medium">
          Logo URL
        </label>

        <Input
          {...register("logoUrl")}
          placeholder="https://example.com/logo.png"
        />

        {errors.logoUrl && (
          <p className="text-sm text-destructive">
            {errors.logoUrl.message}
          </p>
        )}
      </div>

      {/* Submit */}
      <Button
        type="submit"
        disabled={updateOrganization.isPending}
      >
        {updateOrganization.isPending
          ? "Saving..."
          : "Save Changes"}
      </Button>
    </form>
  );
}