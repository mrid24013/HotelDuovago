using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class UserRoleRepository
    {
        public List<UserRole> GetAll()
        {
            List<UserRole> userRoles = new List<UserRole>() { };
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        iUserRole,
                        UserRoleName,
                        UserRoleDescription
                    FROM UserRoles
                    ORDER BY UserRoleName ASC;
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
                                userRoles.Add(new UserRole()
                                {
                                    iUserRole = reader.GetGuid(reader.GetOrdinal("iUserRole")),
                                    UserRoleName = reader.GetString(reader.GetOrdinal("UserRoleName")),
                                    UserRoleDescription = reader.IsDBNull(reader.GetOrdinal("UserRoleDescription"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("UserRoleDescription"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                userRoles = new List<UserRole>() { };
            }

            return userRoles;
        }

        public bool Find(UserRole userRole)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        iUserRole,
                        UserRoleName,
                        UserRoleDescription
                    FROM UserRoles
                    WHERE iUserRole = @iUserRole;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iUserRole", userRole.iUserRole);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                userRole.UserRoleName = reader.GetString(reader.GetOrdinal("UserRoleName"));
                                userRole.UserRoleDescription = reader.IsDBNull(reader.GetOrdinal("UserRoleDescription"))
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

        public bool Insert(UserRole userRole)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO UserRoles (
                        iUserRole,
                        UserRoleName,
                        UserRoleDescription
                    )
                    VALUES (
                        @iUserRole,
                        @UserRoleName,
                        @UserRoleDescription
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iUserRole", userRole.iUserRole);
                        insertCommand.Parameters.AddWithValue("@UserRoleName", userRole.UserRoleName);
                        insertCommand.Parameters.AddWithValue("@UserRoleDescription", (object?)userRole.UserRoleDescription ?? DBNull.Value);
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

        public bool Update(UserRole userRole)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    UPDATE UserRoles
                    SET
                        UserRoleName = @UserRoleName,
                        UserRoleDescription = @UserRoleDescription
                    WHERE iUserRole = @iUserRole;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@iUserRole", userRole.iUserRole);
                        updateCommand.Parameters.AddWithValue("@UserRoleName", userRole.UserRoleName);
                        updateCommand.Parameters.AddWithValue("@UserRoleDescription", (object?)userRole.UserRoleDescription ?? DBNull.Value);
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

        public bool Delete(Guid userRoleId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM UserRoles
                    WHERE iUserRole = @iUserRole;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@iUserRole", userRoleId);
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
