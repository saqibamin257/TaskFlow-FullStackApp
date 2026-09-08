"use client";

import { ArrowLeft } from "lucide-react";
import Link from "next/link";
import { useSearchParams } from "next/navigation";
import { OrganizationDetails } from "./organization-details";

export function OrganizationDetailsPageClient() {
  const searchParams = useSearchParams();
  const id = searchParams.get("id");

  if (!id) {
    return (
      <div className="rounded-xl border p-6">
        <p className="text-destructive">Organization ID is required.</p>

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

  return <OrganizationDetails id={id} />;
}
