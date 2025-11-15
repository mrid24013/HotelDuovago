using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class InspectionDetailRepository
    {
        public List<InspectionDetail> GetAll()
        {
            List<InspectionDetail> details = new List<InspectionDetail>() { };
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        d.iInspectionDetail,
                        d.iInspectionHeader,
                        d.iInventory,
                        d.InspectionDetailExpectedQuantity,
                        d.InspectionDetailRealQuantity,
                        i.iLocation,
                        i.iProduct,
                        l.LocationNumber,
                        l.LocationName,
                        l.LocationDescription,
                        l.LocationEnabled,
                        p.ProductCode,
                        p.ProductDescription,
                        p.ProductPrice,
                        p.iManufacturer
                    FROM InspectionDetails d
                    INNER JOIN Inventories i ON d.iInventory = i.iInventory
                    INNER JOIN Locations l ON i.iLocation = l.iLocation
                    INNER JOIN Products p ON i.iProduct = p.iProduct
                    ORDER BY d.iInspectionDetail DESC;
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
                                details.Add(new InspectionDetail()
                                {
                                    iInspectionDetail = reader.GetGuid(reader.GetOrdinal("iInspectionDetail")),
                                    iInspectionHeader = reader.GetGuid(reader.GetOrdinal("iInspectionHeader")),
                                    iInventory = reader.GetGuid(reader.GetOrdinal("iInventory")),

                                    InspectionDetailExpectedQuantity = reader.GetInt32(reader.GetOrdinal("InspectionDetailExpectedQuantity")),
                                    InspectionDetailRealQuantity = reader.GetInt32(reader.GetOrdinal("InspectionDetailRealQuantity")),

                                    // Location
                                    iLocation = reader.IsDBNull(reader.GetOrdinal("iLocation"))
                                        ? null
                                        : reader.GetGuid(reader.GetOrdinal("iLocation")),
                                    LocationNumber = reader.IsDBNull(reader.GetOrdinal("LocationNumber"))
                                        ? null
                                        : reader.GetInt32(reader.GetOrdinal("LocationNumber")),
                                    LocationName = reader.IsDBNull(reader.GetOrdinal("LocationName"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("LocationName")),

                                    // Product
                                    iProduct = reader.IsDBNull(reader.GetOrdinal("iProduct"))
                                        ? null
                                        : reader.GetGuid(reader.GetOrdinal("iProduct")),
                                    ProductCode = reader.IsDBNull(reader.GetOrdinal("ProductCode"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("ProductCode")),
                                    ProductDescription = reader.IsDBNull(reader.GetOrdinal("ProductDescription"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("ProductDescription")),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                details = new List<InspectionDetail>() { };
            }

            return details;
        }

        public bool Find(InspectionDetail detail)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        d.iInspectionDetail,
                        d.iInspectionHeader,
                        d.iInventory,
                        d.InspectionDetailExpectedQuantity,
                        d.InspectionDetailRealQuantity,
                        i.iLocation,
                        i.iProduct,
                        l.LocationNumber,
                        l.LocationName,
                        l.LocationDescription,
                        l.LocationEnabled,
                        p.ProductCode,
                        p.ProductDescription,
                        p.ProductPrice,
                        p.iManufacturer
                    FROM InspectionDetails d
                    INNER JOIN Inventories i ON d.iInventory = i.iInventory
                    INNER JOIN Locations l ON i.iLocation = l.iLocation
                    INNER JOIN Products p ON i.iProduct = p.iProduct
                    WHERE d.iInspectionDetail = @id;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        // agregamos los parametros
                        command.Parameters.AddWithValue("@id", detail.iInspectionDetail);

                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                detail.iInspectionDetail = reader.GetGuid(reader.GetOrdinal("iInspectionDetail"));
                                detail.iInspectionHeader = reader.GetGuid(reader.GetOrdinal("iInspectionHeader"));
                                detail.iInventory = reader.GetGuid(reader.GetOrdinal("iInventory"));

                                detail.InspectionDetailExpectedQuantity = reader.GetInt32(reader.GetOrdinal("InspectionDetailExpectedQuantity"));
                                detail.InspectionDetailRealQuantity = reader.GetInt32(reader.GetOrdinal("InspectionDetailRealQuantity"));

                                // Location
                                detail.iLocation = reader.IsDBNull(reader.GetOrdinal("iLocation"))
                                    ? null
                                    : reader.GetGuid(reader.GetOrdinal("iLocation"));
                                detail.LocationNumber = reader.IsDBNull(reader.GetOrdinal("LocationNumber"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("LocationNumber"));
                                detail.LocationName = reader.IsDBNull(reader.GetOrdinal("LocationName"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("LocationName"));

                                // Product
                                detail.iProduct = reader.IsDBNull(reader.GetOrdinal("iProduct"))
                                    ? null
                                    : reader.GetGuid(reader.GetOrdinal("iProduct"));
                                detail.ProductCode = reader.IsDBNull(reader.GetOrdinal("ProductCode"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("ProductCode"));
                                detail.ProductDescription = reader.IsDBNull(reader.GetOrdinal("ProductDescription"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("ProductDescription"));
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

        public bool Insert(InspectionDetail detail)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO InspectionDetails (
                        iInspectionDetail,
                        iInspectionHeader,
                        iInventory,
                        InspectionDetailExpectedQuantity,
                        InspectionDetailRealQuantity
                    )
                    VALUES (
                        @iInspectionDetail,
                        @iInspectionHeader,
                        @iInventory,
                        @InspectionDetailExpectedQuantity,
                        @InspectionDetailRealQuantity
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iInspectionDetail", detail.iInspectionDetail);
                        insertCommand.Parameters.AddWithValue("@iInspectionHeader", detail.iInspectionHeader);
                        insertCommand.Parameters.AddWithValue("@iInventory", detail.iInventory);
                        insertCommand.Parameters.AddWithValue("@InspectionDetailExpectedQuantity", detail.InspectionDetailExpectedQuantity);
                        insertCommand.Parameters.AddWithValue("@InspectionDetailRealQuantity", (object?)detail.InspectionDetailRealQuantity ?? DBNull.Value);

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

        public bool Update(InspectionDetail detail)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    UPDATE InspectionDetails
                    SET
                        iInspectionHeader = @iInspectionHeader,
                        iInventory = @iInventory,
                        InspectionDetailExpectedQuantity = @InspectionDetailExpectedQuantity,
                        InspectionDetailRealQuantity = @InspectionDetailRealQuantity
                    WHERE iInspectionDetail = @id;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@id", detail.iInspectionDetail);
                        updateCommand.Parameters.AddWithValue("@iInspectionHeader", detail.iInspectionHeader);
                        updateCommand.Parameters.AddWithValue("@iInventory", detail.iInventory);
                        updateCommand.Parameters.AddWithValue("@InspectionDetailExpectedQuantity", detail.InspectionDetailExpectedQuantity);
                        updateCommand.Parameters.AddWithValue("@InspectionDetailRealQuantity", (object?)detail.InspectionDetailRealQuantity ?? DBNull.Value);

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

        public bool Delete(Guid detailId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM InspectionDetails
                    WHERE iInspectionDetail = @id;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@id", detailId);
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

