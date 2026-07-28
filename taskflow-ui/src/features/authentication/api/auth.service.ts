import api from "@/lib/axios";
import type { LoginRequest, LoginResponse } from "./auth.types";
import { authStorage } from "../storage/auth.storage";

export const authService = {
  // function-1
  async login(request: LoginRequest) {
    const response = await api.post<LoginResponse>(
      "/Authentication/login",
      request,
      {
        skipAuth: true,
      },
    );
    authStorage.storeAccessToken(response.data.accessToken, request.rememberMe);
    return response.data.accessToken;
  },

  // function-2
  logout() {
    authStorage.removeAccessToken();

    window.location.href = "/login";
  },

  //function-3
  async refreshAccessToken(): Promise<string> {
    throw new Error("Refresh token API is not implemented yet.");
  },
};
export const refreshAccessToken = {};
