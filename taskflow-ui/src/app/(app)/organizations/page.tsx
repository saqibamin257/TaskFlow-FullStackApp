import { CreateOrganizationDialog } from "@/features/organizations/components/create-organization-dialog";
import { OrganizationList } from "@/features/organizations/components/organization-list";

export default function OrganizationsPage() {
  return (
    <div className="space-y-8">
      <div>
        <h1 className="text-2xl font-semibold tracking-tight">Organizations</h1>

        <p className="text-sm text-muted-foreground">
          Manage your organizations and their settings.
        </p>
      </div>

      <div className="max-w-xl rounded-lg border p-6">
        <h2 className="mb-5 text-lg font-semibold">Create Organization</h2>

        <CreateOrganizationDialog />
      </div>

      <OrganizationList />
    </div>
  );
}
