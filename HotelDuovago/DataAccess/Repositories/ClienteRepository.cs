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
                        ShipperID,
                        CompanyName,
                        Phone
                    FROM Clientes
                    ORDER BY CompanyName ASC;
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
                                    ShipperID = reader.GetInt32(reader.GetOrdinal("ShipperID")),
                                    CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                                    Phone = reader.GetString(reader.GetOrdinal("Phone"))
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

        public bool Find(Cliente cliente)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        ShipperID,
                        CompanyName,
                        Phone
                    FROM Clientes
                    WHERE ShipperID = @ShipperID;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ShipperID", cliente.ShipperID);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                cliente.CompanyName = reader.GetString(reader.GetOrdinal("CompanyName"));
                                cliente.Phone = reader.GetString(reader.GetOrdinal("Phone"));
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

        public bool FindID(int ShipperID)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        ShipperID
                    FROM Clientes
                    WHERE ShipperID = @ShipperID;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ShipperID", ShipperID);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                ShipperID = reader.GetInt32(reader.GetOrdinal("ShipperID"));
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

        public bool FindCompany(string CompanyName)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        CompanyName
                    FROM Clientes
                    WHERE CompanyName = @CompanyName;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyName", CompanyName.ToLower());
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                CompanyName = reader.GetString(reader.GetOrdinal("CompanyName"));
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

        public bool FindPhone(string Phone)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        Phone
                    FROM Clientes
                    WHERE Phone = @Phone;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Phone", Phone.ToLower());
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Phone = reader.GetString(reader.GetOrdinal("Phone"));
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

        public bool Insert(Cliente cliente)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO Clientes (
                        CompanyName,
                        Phone
                    )
                    VALUES (
                        @CompanyName,
                        @Phone
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@CompanyName", cliente.CompanyName);
                        insertCommand.Parameters.AddWithValue("@Phone", cliente.Phone);
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
                        CompanyName = @CompanyName,
                        Phone = @Phone
                    WHERE ShipperID = @ShipperID;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@ShipperID", cliente.ShipperID);
                        updateCommand.Parameters.AddWithValue("@CompanyName", cliente.CompanyName);
                        updateCommand.Parameters.AddWithValue("@Phone", cliente.Phone);
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

        public bool Delete(int shipperID)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM Clientes
                    WHERE ShipperID = @ShipperID;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@ShipperID", shipperID);
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
