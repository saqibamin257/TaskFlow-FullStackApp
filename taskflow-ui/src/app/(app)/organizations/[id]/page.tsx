import { OrganizationDetails } from "@/features/organizations/components/organization-details";

interface OrganizationPageProps {
  params: Promise<{
    id: string;
  }>;
}

export default async function OrganizationPage({
  params,
}: OrganizationPageProps) {
  const { id } = await params;

  return <OrganizationDetails id={id} />;
}
