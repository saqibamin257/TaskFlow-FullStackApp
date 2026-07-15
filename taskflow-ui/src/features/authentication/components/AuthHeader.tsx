/**
 * AuthHeader
 *
 * Displays the application logo,
 * page title and subtitle.
 *
 * Used by:
 * - Login
 * - Register
 * - Forgot Password
 * - Reset Password
 */

export function AuthHeader() {
  return (
    <div className="flex flex-col items-center gap-2 text-center">
      <h1 className="text-3xl font-bold">TaskFlow</h1>

      <h2 className="text-xl font-semibold">Welcome Back</h2>

      <p className="text-muted-foreground">
        Sign in to continue to your workspace.
      </p>
    </div>
  );
}
