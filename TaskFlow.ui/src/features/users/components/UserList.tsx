import { useUsers } from "../hooks/useUsers";
import { useUpdateUser } from "../hooks/useUpdateUser";
import { useDeleteUser } from "../hooks/useDeleteUser";

export const UserList = () => {
  const { data: users, isLoading, error, isFetching } = useUsers();
  const { mutate: updateUser, isPending: isUpdating } = useUpdateUser();
  const { mutate: deleteUser, isPending: isDeleting } = useDeleteUser();

  if (isLoading) return <p>Loading users...</p>;

  if (error) return <p style={{ color: "red" }}>Failed to load users</p>;

  return (
    <div>
      <h2>Users</h2>

      {/* Background refetch indicator */}
      {isFetching && <p style={{ fontSize: "12px" }}>Refreshing...</p>}

      {/* Empty state */}
      {users?.length === 0 ? (
        <p>No users found</p>
      ) : (
        users?.map((u) => (
          <div
            key={u.id}
            style={{
              border: "1px solid #ccc",
              padding: "10px",
              marginBottom: "10px",
            }}
          >
            <div>
              <strong>{u.name}</strong> - {u.email}
            </div>

            <div style={{ marginTop: "8px" }}>
              {/* UPDATE BUTTON */}
              <button
                onClick={() =>
                  updateUser({
                    id: u.id,
                    name: u.name + " (Updated)",
                    email: u.email,
                  })
                }
                disabled={isUpdating}
                style={{ marginRight: "10px" }}
              >
                {isUpdating ? "Updating..." : "Update"}
              </button>

              {/* DELETE BUTTON */}
              <button
                onClick={() => {
                  if (confirm("Are you sure you want to delete?")) {
                    deleteUser(u.id);
                  }
                }}
                disabled={isDeleting}
              >
                {isDeleting ? "Deleting..." : "Delete"}
              </button>
            </div>
          </div>
        ))
      )}
    </div>
  );
};
