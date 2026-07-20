import axios from "axios";
import { authStorage } from "@/features/authentication/storage/auth.storage";

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  timeout: 30000, // cancel the request after 30 sec if the browser hangs
  headers: {
    "Content-Type": "application/json",
  },
});

// Request Interceptor
api.interceptors.request.use(
  (config) => {
    // Skip authentication for public endpoints
    if (config.skipAuth) {
      return config;
    }

    const token = authStorage.getAccessToken();

    if (token) {
      config.headers.set("Authorization", `Bearer ${token}`);
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

export default api;
