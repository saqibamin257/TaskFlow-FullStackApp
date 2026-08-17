export const organizationKeys = {
  all: ["organizations"] as const,

  list: () => [...organizationKeys.all, "list"] as const,

  detail: (id: string) =>
    [...organizationKeys.all, "detail", id] as const,
};