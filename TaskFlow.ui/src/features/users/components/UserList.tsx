import type { UserDTO } from "../types/user";
import { getUsers } from "../services/userService";
import { useState, useEffect } from "react";

export const UserList = () => {
  const [users, setUsers] = useState<UserDTO[]>([]);

  useEffect(() => {
    getUsers().then(setUsers);
  }, []);

  return (
    <div>
      {users.map((u) => (
        <div key={u.id}>
          <p>
            {u.name} -- {u.email}
          </p>
        </div>
      ))}
    </div>
  );
};
