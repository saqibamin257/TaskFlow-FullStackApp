import { getUsers } from "../../services/userService";
import type { UserDTO } from "../../types/user";
import { useEffect, useState } from "react";

export const UserList = () => {
  const [users, setUsers] = useState<UserDTO[]>([]);

  useEffect(() => {
    getUsers().then(setUsers);
  }, []);

  return (
    <div>
      <h2>User</h2>
      <h3>Test CI Push</h3>
      {users.map((u) => (
        <div key={u.id}>
          {u.name}|{u.email}
        </div>
      ))}
    </div>
  );
};
