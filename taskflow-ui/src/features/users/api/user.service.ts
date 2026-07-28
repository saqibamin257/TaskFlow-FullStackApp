import api from "@/lib/axios";
import type { User } from "../types/user.types";

export const userService = {
  async getCurrentUser(): Promise<User> {
    const response = await api.get<User>("/user/me");    //get details of logged in user
    return response.data;
  },
};
