using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class InspectionHeaderRepository
    {

        public List<InspectionHeader> GetAll()
        {
            List<InspectionHeader> headers = new List<InspectionHeader>() { };
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        h.iInspectionHeader,
                        h.iCreatedBy,
                        h.iLocation,
                        h.InspectionHeaderNumber,
                        h.InspectionHeaderDescription,
                        h.InspectionHeaderCreatedAt,
                        -- Location
                        l.LocationNumber,
                        l.LocationName,
                        l.LocationDescription,
                        l.LocationEnabled,
                        -- User (Creator)
                        u.iUser,
                        u.UserNumber,
                        u.UserFullName,
                        u.UserEmail
                    FROM InspectionHeaders h
                    INNER JOIN Locations l ON h.iLocation = l.iLocation
                    INNER JOIN Users u ON h.iCreatedBy = u.iUser
                    ORDER BY h.InspectionHeaderCreatedAt DESC;
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
                                headers.Add(new InspectionHeader()
                                {
                                    iInspectionHeader = reader.GetGuid(reader.GetOrdinal("iInspectionHeader")),
                                    InspectionHeaderNumber = reader.GetInt32(reader.GetOrdinal("InspectionHeaderNumber")),
                                    InspectionHeaderDescription = reader.GetString(reader.GetOrdinal("InspectionHeaderDescription")),
                                    InspectionHeaderCreatedAt = reader.GetDateTime(reader.GetOrdinal("InspectionHeaderCreatedAt")),

                                    // Created By
                                    iCreatedBy = reader.GetGuid(reader.GetOrdinal("iUser")),
                                    CreatorNumber = reader.GetInt32(reader.GetOrdinal("UserNumber")),
                                    CreatorFullName = reader.IsDBNull(reader.GetOrdinal("UserFullName"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("UserFullName")),
                                    CreatorEmail = reader.IsDBNull(reader.GetOrdinal("UserEmail"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("UserEmail")),

                                    // Location
                                    iLocation = reader.GetGuid(reader.GetOrdinal("iLocation")),
                                    LocationNumber = reader.IsDBNull(reader.GetOrdinal("LocationNumber"))
                                        ? null
                                        : reader.GetInt32(reader.GetOrdinal("LocationNumber")),
                                    LocationName = reader.IsDBNull(reader.GetOrdinal("LocationName"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("LocationName")),
                                    LocationDescription = reader.IsDBNull(reader.GetOrdinal("LocationDescription"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("LocationDescription")),
                                    LocationEnabled = reader.IsDBNull(reader.GetOrdinal("LocationEnabled"))
                                        ? null
                                        : reader.GetBoolean(reader.GetOrdinal("LocationEnabled")),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                headers = new List<InspectionHeader>() { };
            }

            return headers;
        }

        public bool Find(InspectionHeader header)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        h.iInspectionHeader,
                        h.iCreatedBy,
                        h.iLocation,
                        h.InspectionHeaderNumber,
                        h.InspectionHeaderDescription,
                        h.InspectionHeaderCreatedAt,
                        l.LocationNumber,
                        l.LocationName,
                        l.LocationDescription,
                        l.LocationEnabled,
                        u.iUser,
                        u.UserNumber,
                        u.UserFullName,
                        u.UserEmail
                    FROM InspectionHeaders h
                    INNER JOIN Locations l ON h.iLocation = l.iLocation
                    INNER JOIN Users u ON h.iCreatedBy = u.iUser
                    WHERE h.iInspectionHeader = @iInspectionHeader;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iInspectionHeader", header.iInspectionHeader);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                header.iInspectionHeader = reader.GetGuid(reader.GetOrdinal("iInspectionHeader"));
                                header.InspectionHeaderNumber = reader.GetInt32(reader.GetOrdinal("InspectionHeaderNumber"));
                                header.InspectionHeaderDescription = reader.GetString(reader.GetOrdinal("InspectionHeaderDescription"));
                                header.InspectionHeaderCreatedAt = reader.GetDateTime(reader.GetOrdinal("InspectionHeaderCreatedAt"));

                                // Created By
                                header.iCreatedBy = reader.GetGuid(reader.GetOrdinal("iUser"));
                                header.CreatorNumber = reader.GetInt32(reader.GetOrdinal("UserNumber"));
                                header.CreatorFullName = reader.IsDBNull(reader.GetOrdinal("UserFullName"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("UserFullName"));
                                header.CreatorEmail = reader.IsDBNull(reader.GetOrdinal("UserEmail"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("UserEmail"));

                                // Location
                                header.iLocation = reader.GetGuid(reader.GetOrdinal("iLocation"));
                                header.LocationNumber = reader.IsDBNull(reader.GetOrdinal("LocationNumber"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("LocationNumber"));
                                header.LocationName = reader.IsDBNull(reader.GetOrdinal("LocationName"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("LocationName"));
                                header.LocationDescription = reader.IsDBNull(reader.GetOrdinal("LocationDescription"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("LocationDescription"));
                                header.LocationEnabled = reader.IsDBNull(reader.GetOrdinal("LocationEnabled"))
                                    ? null
                                    : reader.GetBoolean(reader.GetOrdinal("LocationEnabled"));
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

        public bool Insert(InspectionHeader header)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO InspectionHeaders (
                        iInspectionHeader,
                        iCreatedBy,
                        iLocation,
                        InspectionHeaderDescription,
                        InspectionHeaderCreatedAt
                    )
                    VALUES (
                        @iInspectionHeader,
                        @iCreatedBy,
                        @iLocation,
                        @InspectionHeaderDescription,
                        @InspectionHeaderCreatedAt
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iInspectionHeader", header.iInspectionHeader);
                        insertCommand.Parameters.AddWithValue("@iCreatedBy", header.iCreatedBy);
                        insertCommand.Parameters.AddWithValue("@iLocation", header.iLocation);
                        insertCommand.Parameters.AddWithValue("@InspectionHeaderDescription", header.InspectionHeaderDescription ?? "");
                        insertCommand.Parameters.AddWithValue("@InspectionHeaderCreatedAt", header.InspectionHeaderCreatedAt);

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

        public bool Update(InspectionHeader header)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    UPDATE InspectionHeaders
                    SET
                        iCreatedBy = @iCreatedBy,
                        iLocation = @iLocation,
                        InspectionHeaderDescription = @InspectionHeaderDescription
                    WHERE iInspectionHeader = @iInspectionHeader;
                ";


                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@iInspectionHeader", header.iInspectionHeader);
                        updateCommand.Parameters.AddWithValue("@iCreatedBy", header.iCreatedBy);
                        updateCommand.Parameters.AddWithValue("@iLocation", header.iLocation);
                        updateCommand.Parameters.AddWithValue("@InspectionHeaderDescription", header.InspectionHeaderDescription ?? "");

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

        public bool Delete(Guid headerId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM InspectionHeaders
                    WHERE iInspectionHeader = @iInspectionHeader;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@iInspectionHeader", headerId);
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
