/**
 * LoginForm
 *
 * Displays authentication controls.
 *
 */
"use client";

import { FormEvent, useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import Link from "next/link";
import { PasswordInput } from "./PasswordInput";
import { RememberMe } from "./RememberMe";

export function LoginForm() {
  const [email, setEmail] = useState("");

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log({ email });
  };

  return (
    <form onSubmit={handleSubmit} className="mt-8 space-y-6">
      {/* Email */}
      <div className="space-y-2">
        <Label htmlFor="email">Email Address</Label>

        <Input
          id="email"
          type="email"
          placeholder="Enter your email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>

      {/* Password */}
      <div className="space-y-2">
        <Label htmlFor="password">Password</Label>

        <PasswordInput />
      </div>

      {/* Remember Me & Forgot Password */}
      <div className="flex items-center justify-between">
        <RememberMe />

        <Link
          href="/forgot-password"
          className="text-sm text-primary hover:underline"
        >
          Forgot Password?
        </Link>
      </div>

      {/* Submit */}
      <Button type="submit" className="w-full">
        Sign In
      </Button>
    </form>
  );
}
