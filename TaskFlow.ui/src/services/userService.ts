import type { UserDTO } from "../types/user";
import { apiClient } from "./apiClient";

export const getUsers = async (): Promise<UserDTO[]> => {
  const resposne = await apiClient.get("user");
  return resposne.data;
};
