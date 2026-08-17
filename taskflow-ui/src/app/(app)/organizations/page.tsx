import { OrganizationList } from "@/features/organizations/components/organization-list";

export default function OrganizationsPage() {
  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-2xl font-semibold tracking-tight">
          Organizations
        </h1>

        <p className="text-sm text-muted-foreground">
          Manage your organizations and their settings.
        </p>
      </div>

      <OrganizationList />
    </div>
  );
}