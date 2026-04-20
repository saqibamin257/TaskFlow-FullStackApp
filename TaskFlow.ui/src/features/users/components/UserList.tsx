import { useUsers } from "../hooks/useUsers";


export const UserList = () => {
  const { users, loading, error } = useUsers();

  if (loading) return <p>Loading users...</p>;
  if (error) return <p style={{ color: "red" }}>{error}</p>;

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
