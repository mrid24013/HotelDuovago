using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class ManufacturerRepository
    {
        public List<Manufacturer> GetAll()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>() { };
            try
            {
                // generamos la query
                string query = "SELECT iManufacturer, ManufacturerCode, ManufacturerName FROM Manufacturers";
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
                                manufacturers.Add(new Manufacturer()
                                {
                                    iManufacturer = reader.GetGuid(reader.GetOrdinal("iManufacturer")),
                                    ManufacturerCode = reader.GetString(reader.GetOrdinal("ManufacturerCode")),
                                    ManufacturerName = reader.GetString(reader.GetOrdinal("ManufacturerName"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                manufacturers = new List<Manufacturer>() { };
            }

            return manufacturers;
        }

        public bool Find(Manufacturer manufacturer)
        {
            try
            {
                // generar query
                string query = "SELECT TOP 1 iManufacturer, ManufacturerCode, ManufacturerName FROM Manufacturers WHERE iManufacturer = @id";
                // obtenemos la conexion de base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // generamos el comando para correr el query
                    using (var command = new SqlCommand(query, connection))
                    {
                        // agregamos los parametros
                        command.Parameters.AddWithValue("@id", manufacturer.iManufacturer);
                        // abrimos la conexion
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                manufacturer.ManufacturerCode = reader.GetString(reader.GetOrdinal("ManufacturerCode"));
                                manufacturer.ManufacturerName = reader.GetString(reader.GetOrdinal("ManufacturerName"));
                                return true;
                            }
                            else
                            {
                                return false;
                            }
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

        public bool Insert(Manufacturer manufacturer)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = "INSERT INTO Manufacturers VALUES (@id, @code, @name)";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        // agregamos los parametros
                        insertCommand.Parameters.AddWithValue("@id", manufacturer.iManufacturer);
                        insertCommand.Parameters.AddWithValue("@code", manufacturer.ManufacturerCode);
                        insertCommand.Parameters.AddWithValue("@name", manufacturer.ManufacturerName);
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

        public bool Update(Manufacturer manufacturer)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = "UPDATE Manufacturers SET ManufacturerCode = @code, ManufacturerName = @name WHERE iManufacturer = @id";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        // agregamos los parametros
                        updateCommand.Parameters.AddWithValue("@code", manufacturer.ManufacturerCode);
                        updateCommand.Parameters.AddWithValue("@name", manufacturer.ManufacturerName);
                        updateCommand.Parameters.AddWithValue("@id", manufacturer.iManufacturer);
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

        public bool Delete(Guid manufacturerId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = "DELETE FROM Manufacturers WHERE iManufacturer = @id";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        // agregamos los parametros
                        deleteCommand.Parameters.AddWithValue("@id", manufacturerId);
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
