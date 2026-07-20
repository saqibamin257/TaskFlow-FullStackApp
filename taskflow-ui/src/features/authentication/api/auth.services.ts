import api from "@/lib/axios";
import type { LoginRequest, LoginResponse } from "./auth.types";
import { authStorage } from "../storage/auth.storage";

export const authService = {
  async login(request: LoginRequest): Promise<void> {
    const response = await api.post<LoginResponse>(
      "/Authentication/login",
      request,
    );

    authStorage.storeAccessToken(response.data.accessToken, request.rememberMe);
  },
};
