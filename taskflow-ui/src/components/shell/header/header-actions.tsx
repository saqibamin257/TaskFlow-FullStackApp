import { Bell } from "lucide-react";
import { UserMenu } from "./user-menu";

export function HeaderActions() {
    return (
        <div className="flex items-center gap-4">

            <button className="rounded-md p-2 hover:bg-muted">
                <Bell className="h-5 w-5" />
            </button>

            <UserMenu />

        </div>
    );
}