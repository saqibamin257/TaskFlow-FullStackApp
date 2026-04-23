import { useCreateUser } from "../hooks/useCreateUser";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { userSchema } from "../schema/userSchema";
import type { UserFormData } from "../schema/userSchema";

export const CreateUser = () => {
  const { mutate, isPending } = useCreateUser();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<UserFormData>({
    resolver: zodResolver(userSchema),
  });

  const onSubmit = (data: UserFormData) => {
    mutate(data);
    reset();
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <h3>Create User</h3>
      <div>
        <input placeholder="Name" {...register("name")} />
        {errors.name && <p style={{ color: "red" }}>{errors.name.message}</p>}
      </div>

      <div>
        <input placeholder="Email" {...register("email")} />
        {errors.email && <p style={{ color: "red" }}>{errors.email.message}</p>}
      </div>

      <button type="submit" disabled={isPending}>
        {isPending ? "Creating..." : "Create"}
      </button>
    </form>
  );
};

// import { useState } from "react";
// import { useCreateUser } from "../hooks/useCreateUser";

// export const CreateUser = () => {
//   const [name, setName] = useState("");
//   const [email, setEmail] = useState("");

//   const { mutate, isPending } = useCreateUser();

//   const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
//     e.preventDefault();
//     mutate({ name, email });
//     setName("");
//     setEmail("");
//   };

//   return (
//     <form onSubmit={handleSubmit}>
//       <h3>Create User</h3>
//       <input
//         placeholder="Enter Name"
//         onChange={(e) => setName(e.target.value)}
//         value={name}
//       />
//       <input
//         placeholder="Enter Email"
//         onChange={(e) => setEmail(e.target.value)}
//         value={email}
//       />
//       <button type="submit" disabled={isPending}>
//         {isPending ? "Creating..." : "Create"}
//       </button>
//     </form>
//   );
// };
