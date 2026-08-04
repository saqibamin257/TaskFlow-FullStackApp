import { authService } from "../api/auth.service";

export function useLogout() {
  const logout = () => {
    authService.logout();
  };
  return {
    logout,
  };
}
