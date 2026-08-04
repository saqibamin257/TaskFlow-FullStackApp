"use client";

import { usePathname } from "next/navigation";

export function HeaderTitle() {
    const pathname = usePathname();

    const title =
        pathname === "/dashboard"
            ? "Dashboard"
            : pathname.replace("/", "");

    return (
        <div>
            <h1 className="text-xl font-semibold capitalize">
                {title}
            </h1>

            <p className="text-sm text-muted-foreground">
                Welcome back.
            </p>
        </div>
    );
}