"use client";
import { Checkbox } from "@/components/ui/checkbox";
import { Label } from "@/components/ui/label";
/**
 * RememberMe
 *
 * Displays the Remember Me option.
 *
 * Used by:
 * - Login
 */

export function RememberMe() {
  return (
    <div className="flex items-center gap-2">
      <Checkbox id="remember-me" />
      <Label htmlFor="remember-me">Remember Me</Label>
    </div>
  );
}
