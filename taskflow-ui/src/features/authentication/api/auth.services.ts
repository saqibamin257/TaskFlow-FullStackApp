import api from "@/lib/axios";
import type { LoginRequest, LoginResponse } from "./auth.types";

export const authService = {
  async login(request: LoginRequest): Promise<void> {
    const response = await api.post<LoginResponse>(
      "/Authentication/login",
      request,
    );

    localStorage.setItem("accessToken", response.data.accessToken);
  },
};
