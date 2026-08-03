import { SidebarFooter } from "./sidebar-footer";
import { SidebarLogo } from "./sidebar-logo";
import { SidebarNavigation } from "./sidebar-navigation";

export function Sidebar() {
  return (
    <aside className="flex w-64 flex-col border-r bg-card">
      <SidebarLogo />

      <div className="flex-1 overflow-y-auto">
        <SidebarNavigation />
      </div>

      <SidebarFooter />
    </aside>
  );
}
