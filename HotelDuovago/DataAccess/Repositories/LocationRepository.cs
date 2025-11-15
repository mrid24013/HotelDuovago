using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class LocationRepository
    {

        public List<Location> GetAll()
        {
            List<Location> locations = new List<Location>() { };
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        iLocation,
                        LocationNumber,
                        LocationName,
                        LocationDescription,
                        LocationEnabled
                    FROM Locations;
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
                                locations.Add(new Location()
                                {
                                    iLocation = reader.GetGuid(reader.GetOrdinal("iLocation")),
                                    LocationNumber = reader.GetInt32(reader.GetOrdinal("LocationNumber")),
                                    LocationName = reader.GetString(reader.GetOrdinal("LocationName")),
                                    LocationDescription = reader.IsDBNull(reader.GetOrdinal("LocationDescription"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("LocationDescription")),
                                    LocationEnabled = reader.GetBoolean(reader.GetOrdinal("LocationEnabled"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                locations = new List<Location>() { };
            }

            return locations;
        }

        public bool Find(Location location)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        iLocation,
                        LocationNumber,
                        LocationName,
                        LocationDescription,
                        LocationEnabled
                    FROM Locations
                    WHERE iLocation = @LocationId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocationId", location.iLocation);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                location.LocationNumber = reader.GetInt32(reader.GetOrdinal("LocationNumber"));
                                location.LocationName = reader.GetString(reader.GetOrdinal("LocationName"));
                                location.LocationDescription = reader.IsDBNull(reader.GetOrdinal("LocationDescription"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("LocationDescription"));
                                location.LocationEnabled = reader.GetBoolean(reader.GetOrdinal("LocationEnabled"));
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

        public bool Insert(Location location)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO Locations (
                        iLocation,
                        LocationName,
                        LocationDescription,
                        LocationEnabled
                    )
                    VALUES (
                        @iLocation,
                        @LocationName,
                        @LocationDescription,
                        @LocationEnabled
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iLocation", location.iLocation);
                        insertCommand.Parameters.AddWithValue("@LocationName", location.LocationName);
                        insertCommand.Parameters.AddWithValue("@LocationDescription", (object?)location.LocationDescription ?? DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@LocationEnabled", location.LocationEnabled ?? true);
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

        public bool Update(Location location)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    UPDATE Locations
                    SET 
                        LocationName = @LocationName,
                        LocationDescription = @LocationDescription,
                        LocationEnabled = @LocationEnabled
                    WHERE iLocation = @iLocation;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@iLocation", location.iLocation);
                        updateCommand.Parameters.AddWithValue("@LocationName", location.LocationName);
                        updateCommand.Parameters.AddWithValue("@LocationDescription", (object?)location.LocationDescription ?? DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@LocationEnabled", location.LocationEnabled ?? true);
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

        public bool Delete(Guid locationId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM Locations
                    WHERE iLocation = @LocationId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@LocationId", locationId);
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
