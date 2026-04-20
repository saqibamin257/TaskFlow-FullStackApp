import { useUsers } from "../hooks/useUsers";
export const UserList = () => {
  const { data: users, isLoading, error, isFetching } = useUsers();

  if (isLoading) return <p>Loading users...</p>;

  if (error) return <p style={{ color: "red" }}>Failed to load users</p>;

  return(
        <div>
         <h2>Users</h2>
         {/* 🔄 Background refetch indicator */}
         {isFetching && <p style={{ fontSize: "12px" }}>Refreshing...</p>}
         {/* 📭 Empty state */}
         {users?.length === 0
         ?
         <p>No users found</p>
         :
         users?.map((u)=>(
         <div key={u.id}>
            <p>
             {u.name} -- {u.email}
           </p>
         </div>
         ))        
        }
        </div>);
};

// import { useUsers } from "../hooks/useUsers";

// export const UserList = () => {
//   const { users, loading, error } = useUsers();

//   if (loading) return <p>Loading users...</p>;
//   if (error) return <p style={{ color: "red" }}>{error}</p>;

//   return (
//     <div>
//       {users.map((u) => (
//         <div key={u.id}>
//           <p>
//             {u.name} -- {u.email}
//           </p>
//         </div>
//       ))}
//     </div>
//   );
// };
