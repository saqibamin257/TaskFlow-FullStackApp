/**
 * LoginForm
 *
 * Displays authentication controls.
 *
 */
"use client";

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import Link from "next/link";
import { PasswordInput } from "./PasswordInput";
import { RememberMe } from "./RememberMe";
import { Controller, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { loginSchema, LoginFormData } from "../schemas/login-schema";

export function LoginForm() {
  const {
    register,
    control,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      email: "",
      password: "",
      rememberMe: false,
    },
  });

  const email = watch("email");
  const password = watch("password");

  const canLogin = email.length > 0 && password.length > 0;

  console.count("LoginForm Render");
  console.log("Errors:", errors);

  const onSubmit = (data: LoginFormData) => {
    console.log(data);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="mt-8 space-y-6">
      {/* Email */}
      <div className="space-y-2">
        <Label htmlFor="email">Email Address</Label>

        <Input
          id="email"
          type="email"
          placeholder="Enter your email"
          {...register("email")}
        />
        {errors.email && (
          <p className="text-sm text-red-500 mt-1">{errors.email.message}</p>
        )}
      </div>

      {/* Password */}
      <div className="space-y-2">
        <Label>Password</Label>

        <Controller
          name="password"
          control={control}
          render={({ field }) => (
            <PasswordInput value={field.value} onChange={field.onChange} />
          )}
        />
        {errors.password && (
          <p className="text-sm text-red-500 mt-1">{errors.password.message}</p>
        )}
      </div>

      {/* Remember Me & Forgot Password */}
      <div className="flex items-center justify-between">
        <Controller
          name="rememberMe"
          control={control}
          render={({ field }) => (
            <RememberMe
              checked={field.value}
              onCheckedChange={field.onChange}
            />
          )}
        />

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
