import {
  FolderKanban,
  Building2,
  LayoutDashboard,
  Settings,
  CheckSquare,
} from "lucide-react";

export const navigation = [
  {
    label: "Dashboard",
    href: "/dashboard",
    icon: LayoutDashboard,
  },
  {
    label: "Organizations",
    href: "/organizations",
    icon: Building2,
  },
  {
    label: "Projects",
    href: "/projects",
    icon: FolderKanban,
  },
  {
    label: "Tasks",
    href: "/tasks",
    icon: CheckSquare,
  },
  {
    label: "Settings",
    href: "/settings",
    icon: Settings,
  },
];
