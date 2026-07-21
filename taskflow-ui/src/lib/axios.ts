/**
 * -----------------------------------------------------------------------------
 * Axios Instance
 * -----------------------------------------------------------------------------
 *
 * Central HTTP client for the entire application.
 *
 * Responsibilities:
 * 1. Configure the base API URL.
 * 2. Apply common request headers.
 * 3. Attach authentication token before every protected request.
 * 4. Handle common API responses (e.g. Unauthorized).
 *
 * NOTE:
 * All services (Auth, Organization, Project, Task, etc.)
 * should use this single Axios instance.
 * -----------------------------------------------------------------------------
 */

import axios from "axios";
import { authService } from "@/features/authentication/api/auth.service";
import { authStorage } from "@/features/authentication/storage/auth.storage";

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  timeout: 30000, // cancel the request after 30 sec if the browser hangs
  headers: {
    "Content-Type": "application/json",
  },
});

/**
 * -----------------------------------------------------------------------------
 * Request Interceptor
 * -----------------------------------------------------------------------------
 *
 * Purpose:
 * Automatically attach the current Access Token to every protected API request.
 *
 * Why?
 * Without an interceptor, every service would need to manually add:
 *
 *      Authorization: Bearer <token>
 *
 * This would duplicate authentication logic throughout the application.
 *
 * How it works:
 * 1. Skip authentication for public endpoints (login, refresh, forgot password).
 * 2. Read the latest access token from AuthStorage.
 * 3. Attach the Authorization header.
 * 4. Continue the request.
 *
 * Similar to:
 * ASP.NET Core Authentication Middleware.
 * -----------------------------------------------------------------------------
 */

api.interceptors.request.use(
  (config) => {
    // Public endpoints do not require an Authorization header.
    if (config.skipAuth) {
      return config;
    }

    // Always read the latest token from storage.
    // This allows retried requests to automatically use a refreshed token.
    const token = authStorage.getAccessToken();

    // Attach the access token to every protected request.
    if (token) {
      config.headers.set("Authorization", `Bearer ${token}`);
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

/**
 * -----------------------------------------------------------------------------
 * Response Interceptor
 * -----------------------------------------------------------------------------
 *
 * Purpose:
 * Centralized authentication recovery.
 *
 * Every API response passes through this interceptor.
 *
 * Responsibilities:
 * 1. Detect Unauthorized (401) responses.
 * 2. Attempt to recover the session by refreshing the access token.
 * 3. Retry the original request after successful refresh.
 * 4. Logout the user if authentication cannot be recovered.
 *
 * Why?
 * Without this interceptor, every API service would need to
 * implement its own authentication recovery logic.
 *
 * Similar to:
 * ASP.NET Core Exception Handling Middleware.
 * -----------------------------------------------------------------------------
 */

/**
 * Ensures only one refresh token request is executed at a time.
 *
 * Multiple requests receiving 401 simultaneously will wait
 * for the same refresh operation instead of creating duplicate
 * refresh requests.
 */

let refreshPromise: Promise<string> | null = null;

api.interceptors.response.use(
  (response) => response,

  async (error) => {
    // Authentication recovery is only required for Unauthorized responses.
    if (error.response?.status !== 401) {
      return Promise.reject(error);
    }

    // Preserve the original request so it can be retried
    // after authentication is successfully restored.
    const originalRequest = error.config;

    // Ignore public endpoints (login, refresh, forgot password).
    // They should never trigger authentication recovery.

    if (!originalRequest || originalRequest.skipAuth) {
      return Promise.reject(error);
    }

    // Prevent infinite retry loops.
    // Every request gets only one refresh attempt.
    if (originalRequest._retry) {
      authService.logout();

      return Promise.reject(error);
    }
    // Mark the request before attempting refresh.
    originalRequest._retry = true;

    // Restore authentication and retry the failed request.
    try {
      if (!refreshPromise) {
        refreshPromise = authService.refreshAccessToken(); //
      }

      // Start a refresh operation only if one is not already running.
      await refreshPromise;

      // Wait for the existing refresh operation (or the one just started).
      return api(originalRequest);
    } catch (refreshError) {
      // Retry the original request.
      // It will pass through the Request Interceptor again,
      // which automatically attaches the latest access token.
      authService.logout();
      return Promise.reject(refreshError);
    } finally {
      // Allow future refresh operations.
      refreshPromise = null;
    }
  },
);

export default api;
