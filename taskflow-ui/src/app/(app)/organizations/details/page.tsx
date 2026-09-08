import { Suspense } from "react";
import { OrganizationDetailsPageClient } from "@/features/organizations/components/organization-details-page-client";

export default function OrganizationDetailsPage() {
  return (
    <Suspense
      fallback={
        <div className="rounded-xl border p-6">
          <p className="text-muted-foreground">Loading organization...</p>
        </div>
      }
    >
      <OrganizationDetailsPageClient />
    </Suspense>
  );
}
