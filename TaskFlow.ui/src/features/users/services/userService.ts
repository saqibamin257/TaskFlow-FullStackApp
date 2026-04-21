import type { UserDTO } from "../types/user";
import { apiClient } from "../../../services/apiClient";

export const getUsers = async (): Promise<UserDTO[]> => {
  const resposne = await apiClient.get("user");
  return resposne.data;
};

export const createUser = async (
  user: Omit<UserDTO, "id">,
): Promise<UserDTO> => {
  const resposne = await apiClient.post("user", user);
  return resposne.data;
};
