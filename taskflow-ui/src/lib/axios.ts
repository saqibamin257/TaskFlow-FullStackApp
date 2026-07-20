import axios from "axios";

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  timeout: 30000, // cancel the request after 30 sec if the browser hangs
  headers: {
    "Content-Type": "application/json",
  },
});

export default api;
