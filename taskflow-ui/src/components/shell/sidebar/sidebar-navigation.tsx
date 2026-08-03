"use client";

import { navigation } from "./navigation";
import { SidebarItem } from "./sidebar-item";

export function SidebarNavigation() {
  return (
    <nav className="flex flex-col gap-1 p-4">
      {navigation.map((navigationItem) => (
        <SidebarItem
          key={navigationItem.href}
          label={navigationItem.label}
          href={navigationItem.href}
          icon={navigationItem.icon}
        />
      ))}
    </nav>
  );
}
