import type { ReactNode } from "react";

import { AppShell } from "@/components/shell/app-shell";

interface AppLayoutProps {
  children: ReactNode;
}

/**
 * Layout for all authenticated application routes.
 *
 * Every page inside the `(app)` route group is rendered
 * within the shared Application Shell.
 */
export default function AppLayout({ children }: Readonly<AppLayoutProps>) {
  return <AppShell>{children}</AppShell>;
}
