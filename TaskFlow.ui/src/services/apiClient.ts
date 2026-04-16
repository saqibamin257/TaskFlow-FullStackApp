import axios from "axios";

export const apiClient = axios.create({
  baseURL: "https://localhost:7111/api/",
  headers: {
    "Content-Type": "application/json",
  },
});
