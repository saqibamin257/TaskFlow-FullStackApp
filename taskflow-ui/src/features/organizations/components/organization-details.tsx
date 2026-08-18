"use client";

import { ArrowLeft, Building2 } from "lucide-react";
import Link from "next/link";

import { useOrganization } from "../hooks/use-organization";

interface OrganizationDetailsProps {
  id: string;
}

export function OrganizationDetails({
  id,
}: OrganizationDetailsProps) {
  const { data: organization, isLoading, isError } = useOrganization(id);

  if (isLoading) {
    return (
      <div className="rounded-xl border p-6">
        <p className="text-muted-foreground">
          Loading organization...
        </p>
      </div>
    );
  }

  if (isError || !organization) {
    return (
      <div className="rounded-xl border p-6">
        <p className="text-destructive">
          Unable to load organization.
        </p>

        <Link
          href="/organizations"
          className="mt-4 inline-flex items-center gap-2 text-sm font-medium hover:underline"
        >
          <ArrowLeft className="h-4 w-4" />
          Back to Organizations
        </Link>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Back navigation */}
      <Link
        href="/organizations"
        className="inline-flex items-center gap-2 text-sm text-muted-foreground hover:text-foreground"
      >
        <ArrowLeft className="h-4 w-4" />
        Back to Organizations
      </Link>

      {/* Organization Header */}
      <div className="rounded-xl border bg-card p-6">
        <div className="flex items-start gap-4">
          <div className="flex h-14 w-14 items-center justify-center rounded-xl bg-primary text-primary-foreground">
            <Building2 className="h-7 w-7" />
          </div>

          <div className="flex-1">
            <div className="flex items-center gap-3">
              <h1 className="text-2xl font-semibold">
                {organization.name}
              </h1>

              <span
                className={`rounded-full px-2.5 py-1 text-xs font-medium ${
                  organization.isActive
                    ? "bg-green-100 text-green-700"
                    : "bg-muted text-muted-foreground"
                }`}
              >
                {organization.isActive ? "Active" : "Inactive"}
              </span>
            </div>

            <p className="mt-1 text-muted-foreground">
              {organization.description}
            </p>
          </div>
        </div>
      </div>

      {/* Organization Information */}
      <div className="rounded-xl border bg-card p-6">
        <h2 className="text-lg font-semibold">
          Organization Information
        </h2>

        <div className="mt-6 grid gap-6 sm:grid-cols-2">
          <div>
            <p className="text-sm text-muted-foreground">
              Slug
            </p>
            <p className="mt-1 font-medium">
              {organization.slug}
            </p>
          </div>

          <div>
            <p className="text-sm text-muted-foreground">
              Owner ID
            </p>
            <p className="mt-1 break-all font-medium">
              {organization.ownerUserId}
            </p>
          </div>

          <div>
            <p className="text-sm text-muted-foreground">
              Created
            </p>
            <p className="mt-1 font-medium">
              {new Date(
                organization.createdAtUTC
              ).toLocaleString()}
            </p>
          </div>

          <div>
            <p className="text-sm text-muted-foreground">
              Updated
            </p>
            <p className="mt-1 font-medium">
              {organization.updatedAtUTC
                ? new Date(
                    organization.updatedAtUTC
                  ).toLocaleString()
                : "Never"}
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}