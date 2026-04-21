import { useState } from "react";
import { useCreateUser } from "../hooks/useCreateUser";

export const CreateUser = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");

  const { mutate, isPending } = useCreateUser();

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    mutate({ name, email });
    setName("");
    setEmail("");
  };

  return (
    <form onSubmit={handleSubmit}>
      <h3>Create User</h3>
      <input
        placeholder="Enter Name"
        onChange={(e) => setName(e.target.value)}
        value={name}
      />
      <input
        placeholder="Enter Email"
        onChange={(e) => setEmail(e.target.value)}
        value={email}
      />
      <button type="submit" disabled={isPending}>
        {isPending ? "Creating..." : "Create"}
      </button>
    </form>
  );
};
