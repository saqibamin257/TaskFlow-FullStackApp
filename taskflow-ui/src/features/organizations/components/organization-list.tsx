"use client";

import { useOrganizations } from "../hooks/use-organizations";

export function OrganizationList() {
  const {
    data: organizations,
    isLoading,
    isError,
    error,
  } = useOrganizations();

  if (isLoading) {
    return <div>Loading organizations...</div>;
  }

  if (isError) {
    return (
      <div className="text-sm text-destructive">
        {error instanceof Error
          ? error.message
          : "Failed to load organizations."}
      </div>
    );
  }

  if (!organizations || organizations.length === 0) {
    return (
      <div className="rounded-lg border border-dashed p-8 text-center">
        <h2 className="text-lg font-semibold">
          No organizations yet
        </h2>

        <p className="mt-2 text-sm text-muted-foreground">
          Create your first organization to get started.
        </p>
      </div>
    );
  }

  return (
    <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
      {organizations.map((organization) => (
        <div
          key={organization.id}
          className="rounded-lg border p-5"
        >
          <h2 className="font-semibold">
            {organization.name}
          </h2>

          <p className="mt-1 text-sm text-muted-foreground">
            {organization.description}
          </p>

          <p className="mt-3 text-xs text-muted-foreground">
            {organization.slug}
          </p>
        </div>
      ))}
    </div>
  );
}