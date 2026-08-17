"use client";

import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

import { useCreateOrganization } from "../hooks/use-create-organization";
import {
  createOrganizationSchema,
  type CreateOrganizationFormData,
} from "../schemas/organization-schema";
import type { CreateOrganizationRequest } from "../types/organization.types";

export function CreateOrganizationForm() {
  const createOrganization = useCreateOrganization();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateOrganizationFormData>({
    resolver: zodResolver(createOrganizationSchema),
    defaultValues: {
      name: "",
      slug: "",
      description: "",
      logoUrl: "",
    },
  });

  const onSubmit = (data: CreateOrganizationFormData) => {
    const request: CreateOrganizationRequest = {
      name: data.name,
      slug: data.slug,
      description: data.description,
      logoUrl: data.logoUrl || null,
    };

    createOrganization.mutate(request, {
      onSuccess: () => {
        reset();
      },
    });
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-5">
      <div className="space-y-2">
        <label htmlFor="name" className="text-sm font-medium">
          Organization Name
        </label>

        <input
          id="name"
          {...register("name")}
          className="w-full rounded-md border px-3 py-2 text-sm"
          placeholder="Pinenix Software Solutions"
        />

        {errors.name && (
          <p className="text-sm text-destructive">{errors.name.message}</p>
        )}
      </div>

      <div className="space-y-2">
        <label htmlFor="slug" className="text-sm font-medium">
          Slug
        </label>

        <input
          id="slug"
          {...register("slug")}
          className="w-full rounded-md border px-3 py-2 text-sm"
          placeholder="pinenix"
        />

        {errors.slug && (
          <p className="text-sm text-destructive">{errors.slug.message}</p>
        )}
      </div>

      <div className="space-y-2">
        <label htmlFor="description" className="text-sm font-medium">
          Description
        </label>

        <textarea
          id="description"
          {...register("description")}
          rows={4}
          className="w-full rounded-md border px-3 py-2 text-sm"
          placeholder="Software Development Company"
        />

        {errors.description && (
          <p className="text-sm text-destructive">
            {errors.description.message}
          </p>
        )}
      </div>

      <div className="space-y-2">
        <label htmlFor="logoUrl" className="text-sm font-medium">
          Logo URL
        </label>

        <input
          id="logoUrl"
          {...register("logoUrl")}
          className="w-full rounded-md border px-3 py-2 text-sm"
          placeholder="https://example.com/logo.png"
        />

        {errors.logoUrl && (
          <p className="text-sm text-destructive">{errors.logoUrl.message}</p>
        )}
      </div>

      {createOrganization.isError && (
        <p className="text-sm text-destructive">
          {createOrganization.error instanceof Error
            ? createOrganization.error.message
            : "Failed to create organization."}
        </p>
      )}

      <button
        type="submit"
        disabled={createOrganization.isPending}
        className="rounded-md bg-primary px-4 py-2 text-sm font-medium text-primary-foreground disabled:cursor-not-allowed disabled:opacity-50"
      >
        {createOrganization.isPending ? "Creating..." : "Create Organization"}
      </button>
    </form>
  );
}
