"use client";
import { useState } from "react";
import { Eye, EyeOff } from "lucide-react";
import { Input } from "@/components/ui/input";
/**
 * PasswordInput
 *
 * Responsibility:
 * Reusable password input with
 * show/hide functionality.
 *
 * Used By:
 * Login
 * Register
 * Reset Password
 * Change Password
 */

type PasswordInputProps = {
  readonly value: string;
  readonly onChange: (value: string) => void;
};

export function PasswordInput({ value, onChange }: PasswordInputProps) {
  const [showPassword, setShowPassword] = useState(false);

  return (
    <div className="relative">
      <Input
        value={value}
        onChange={(e) => onChange(e.target.value)}
        type={showPassword ? "text" : "password"}
        placeholder="Enter your password"
      />

      <button
        type="button"
        onClick={() => setShowPassword((previous) => !previous)} //recommended React patterent whenever the next state depends on previous state.
        className="
        absolute
        right-3
        top-1/2
        -translate-y-1/2
        text-muted-foreground
        hover:text-foreground
        transition-colors
    "
        aria-label={showPassword ? "Hide password" : "Show password"}
      >
        {showPassword ? (
          <EyeOff className="h-4 w-4" />
        ) : (
          <Eye className="h-4 w-4" />
        )}
      </button>
    </div>
  );
}
