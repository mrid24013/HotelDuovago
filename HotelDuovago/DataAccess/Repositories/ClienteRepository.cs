using DataAccess.Connections;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess.Repositories
{
    public class ClienteRepository
    {
        public List<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>() { };
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        ClienteId,
                        Nombre,
                        Telefono,
                        Email,
                        Direccion,
                        FechaRegistro
                    FROM Clientes
                    ORDER BY Nombre ASC;
                ";
                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
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
                                clientes.Add(new Cliente()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                    FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                clientes = new List<Cliente>() { };
            }

            return clientes;
        }

        //=====================================================================================================================

        public bool Find(Cliente cliente)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        ClienteId,
                        Nombre,
                        Telefono,
                        Email,
                        Direccion,
                        FechaRegistro
                    FROM Clientes
                    WHERE ClienteId = @ClienteId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteId", cliente.Id);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                cliente.Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                                cliente.Telefono = reader.GetString(reader.GetOrdinal("Telefono"));
                                cliente.Email = reader.GetString(reader.GetOrdinal("Email"));
                                cliente.Direccion = reader.GetString(reader.GetOrdinal("Direccion"));
                                cliente.FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro"));
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

        public bool FindID(int ClienteId)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        ClienteId
                    FROM Clientes
                    WHERE ClienteId = @ClienteId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteId", ClienteId);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
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

        public bool FindNombre(string Nombre)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        Nombre
                    FROM Clientes
                    WHERE Nombre = @Nombre;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", Nombre.ToLower());
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
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

        public bool FindTelefono(string Telefono)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        Telefono
                    FROM Clientes
                    WHERE Telefono = @Telefono;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Telefono", Telefono.ToLower());
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Telefono = reader.GetString(reader.GetOrdinal("Telefono"));
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

        public bool FindEmail(string Email)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        Email
                    FROM Clientes
                    WHERE Email = @Email;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Email.ToLower());
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Email = reader.GetString(reader.GetOrdinal("Email"));
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

        public bool FindDireccion(string Direccion)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        Direccion
                    FROM Clientes
                    WHERE Direccion = @Direccion;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Direccion", Direccion.ToLower());
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Direccion = reader.GetString(reader.GetOrdinal("Direccion"));
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

        public bool FindFechaRegistro(DateTime FechaRegistro)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        FechaRegistro
                    FROM Clientes
                    WHERE FechaRegistro = @FechaRegistro;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FechaRegistro", FechaRegistro);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro"));
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

        //=====================================================================================================================

        public bool Insert(Cliente cliente)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO Clientes (
                        Nombre,
                        Telefono,
                        Email,
                        Direccion,
                        FechaRegistro
                    )
                    VALUES (
                        @Nombre,
                        @Telefono,
                        @Email,
                        @Direccion,
                        @FechaRegistro
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        insertCommand.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        insertCommand.Parameters.AddWithValue("@Email", cliente.Email);
                        insertCommand.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        insertCommand.Parameters.AddWithValue("@FechaRegistro", cliente.FechaRegistro);
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

        public bool Update(Cliente cliente)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    UPDATE Clientes
                    SET
                        Nombre = @Nombre,
                        Telefono = @Telefono,
                        Email = @Email,
                        Direccion = @Direccion,
                        FechaRegistro = @FechaRegistro
                    WHERE ClienteId = @ClienteId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@ClienteId", cliente.Id);
                        updateCommand.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        updateCommand.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        updateCommand.Parameters.AddWithValue("@Email", cliente.Email);
                        updateCommand.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        updateCommand.Parameters.AddWithValue("@FechaRegistro", cliente.FechaRegistro);
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

        public bool Delete(int ClienteId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM Clientes
                    WHERE ClienteId = @ClienteId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@ClienteId", ClienteId);
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
