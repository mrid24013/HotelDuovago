using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class UserRepository
    {
        public List<User> GetAll()
        {
            List<User> users = new List<User>() { };
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        U.iUser,
                        U.UserNumber,
                        U.UserFullName,
                        U.UserEmail,
                        U.UserPassword,
                        U.iUserRole,
                        R.UserRoleName,
                        R.UserRoleDescription
                    FROM Users U
                    INNER JOIN UserRoles R ON U.iUserRole = R.iUserRole
                    ORDER BY U.UserNumber ASC;
                ";
                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        // abrimos la conexion 
                        connection.Open();
                        // executamos la query y mapeamos el resultado
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // itera en base al total de filas obtenidas en el reader
                            while (reader.Read())
                            {
                                users.Add(new User()
                                {
                                    iUser = reader.GetGuid(reader.GetOrdinal("iUser")),
                                    UserNumber = reader.GetInt32(reader.GetOrdinal("UserNumber")),
                                    UserFullName = reader.GetString(reader.GetOrdinal("UserFullName")),
                                    UserEmail = reader.IsDBNull(reader.GetOrdinal("UserEmail"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("UserEmail")),
                                    UserPassword = reader.IsDBNull(reader.GetOrdinal("UserPassword"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("UserPassword")),
                                    iUserRole = reader.GetGuid(reader.GetOrdinal("iUserRole")),
                                    UserRoleName = reader.IsDBNull(reader.GetOrdinal("UserRoleName"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("UserRoleName")),
                                    UserRoleDescription = reader.IsDBNull(reader.GetOrdinal("UserRoleDescription"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("UserRoleDescription")),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                users = new List<User>() { };
            }

            return users;
        }

        public bool Find(User user)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        U.iUser,
                        U.UserNumber,
                        U.UserFullName,
                        U.UserEmail,
                        U.UserPassword,
                        U.iUserRole,
                        R.UserRoleName,
                        R.UserRoleDescription
                    FROM Users U
                    INNER JOIN UserRoles R ON U.iUserRole = R.iUserRole
                    WHERE U.iUser = @iUser;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iUser", user.iUser);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                user.iUser = reader.GetGuid(reader.GetOrdinal("iUser"));
                                user.UserNumber = reader.GetInt32(reader.GetOrdinal("UserNumber"));
                                user.UserFullName = reader.GetString(reader.GetOrdinal("UserFullName"));
                                user.UserEmail = reader.IsDBNull(reader.GetOrdinal("UserEmail"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("UserEmail"));
                                user.UserPassword = reader.IsDBNull(reader.GetOrdinal("UserPassword"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("UserPassword"));
                                user.iUserRole = reader.GetGuid(reader.GetOrdinal("iUserRole"));
                                user.UserRoleName = reader.IsDBNull(reader.GetOrdinal("UserRoleName"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("UserRoleName"));
                                user.UserRoleDescription = reader.IsDBNull(reader.GetOrdinal("UserRoleDescription"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("UserRoleDescription"));
                                return true;
                            }

                            // si el reader no obtuvo nada, no se encontró
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Insert(User user)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO Users (
                        iUser,
                        UserFullName,
                        UserEmail,
                        UserPassword,
                        iUserRole
                    )
                    VALUES (
                        @iUser,
                        @UserFullName,
                        @UserEmail,
                        @UserPassword,
                        @iUserRole
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iUser", user.iUser);
                        insertCommand.Parameters.AddWithValue("@UserFullName", user.UserFullName);
                        insertCommand.Parameters.AddWithValue("@UserEmail", (object?)user.UserEmail ?? DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@UserPassword", (object?)user.UserPassword ?? DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@iUserRole", user.iUserRole);
                        // abrimos la conexion 
                        connection.Open();
                        // obten la cantidad de filas afectadas
                        int result = insertCommand.ExecuteNonQuery();
                        // cerramos la conexion
                        connection.Close();
                        // Si se insertó correctamente 
                        // debe existir al menos una fila afectada
                        return result > 0;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(User user)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    UPDATE Users
                    SET
                        UserFullName = @UserFullName,
                        UserEmail = @UserEmail,
                        UserPassword = @UserPassword,
                        iUserRole = @iUserRole
                    WHERE iUser = @iUser;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@iUser", user.iUser);
                        updateCommand.Parameters.AddWithValue("@UserFullName", user.UserFullName);
                        updateCommand.Parameters.AddWithValue("@UserEmail", (object?)user.UserEmail ?? DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@UserPassword", (object?)user.UserPassword ?? DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@iUserRole", user.iUserRole);
                        // abrimos la conexion
                        connection.Open();
                        // obten la cantidad de filas afectadas
                        int result = updateCommand.ExecuteNonQuery();
                        // cerramos la conexion
                        connection.Close();

                        // Si se actualizo correctamente 
                        // debe existir al menos una fila afectada
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Delete(Guid userId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM Users
                    WHERE iUser = @iUser;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@iUser", userId);
                        // abrimos la conexion
                        connection.Open();
                        // obten la cantidad de filas afectadas
                        int result = deleteCommand.ExecuteNonQuery();
                        // cerramos la conexion
                        connection.Close();

                        // Si se actualizo correctamente 
                        // debe existir al menos una fila afectada
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
