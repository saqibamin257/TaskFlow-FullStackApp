import { HeaderTitle } from "./header-title";
import { HeaderActions } from "./header-actions";

export function Header() {
    return (
        <header className="flex h-16 items-center justify-between border-b bg-background px-6">
            <HeaderTitle />

            <HeaderActions />
        </header>
    );
}