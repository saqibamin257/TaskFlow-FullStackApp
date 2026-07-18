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

type RememberMeProps = {
  readonly checked: boolean;
  readonly onCheckedChange: (checked: boolean) => void;
};

export function RememberMe({ checked, onCheckedChange }: RememberMeProps) {
  return (
    <div className="flex items-center space-x-2">
      <Checkbox
        id="remember-me"
        checked={checked}
        onCheckedChange={(value) => onCheckedChange(Boolean(value))}
      />
      <Label htmlFor="remember-me">Remember Me</Label>
    </div>
  );
}
