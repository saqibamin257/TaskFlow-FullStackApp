//One Responsibility Compose the Layout only Not authentication, navigation data, state, permissions, Only composition.

import { ReactNode } from "react";

import { Header } from "./header/header";
import { PageContainer } from "./page-container/page-container";
import { Sidebar } from "./sidebar/sidebar";

interface AppShellProps {
  children: ReactNode;
}

export function AppShell({ children }: AppShellProps) {
  return (
    <div className="flex min-h-screen bg-background">
      <Sidebar />

      <div className="flex flex-1 flex-col">
        <Header />

        <PageContainer>{children}</PageContainer>
      </div>
    </div>
  );
}
