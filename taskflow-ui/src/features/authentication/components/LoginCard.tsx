import { AuthHeader } from "./AuthHeader";
import { LoginForm } from "./LoginForm";

export function LoginCard() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-slate-100 p-4">
      <div className="w-full max-w-md rounded-xl border bg-white p-8 shadow-lg ">
        <AuthHeader />
        <LoginForm />
      </div>
    </div>
  );
}
