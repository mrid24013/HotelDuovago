using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class MovementRequestDetailRepository
    {
        public List<MovementRequestDetail> GetAll()
        {
            List<MovementRequestDetail> details = new List<MovementRequestDetail>();
            try
            {
                string query = @"
                    SELECT
                        MRD.iMovementRequestDetail,
                        MRD.iMovementRequestHeader,
                        MRD.iMovementType,
                        MRD.iInventory,
                        MRD.MovementRequestDetailDescription,
                        MRD.MovementRequestDetailQuantity,
                        MRD.MovementRequestDetailStatus,

                        -- Header
                        MRH.MovementRequestHeaderNumber,
                        MRH.MovementRequestHeaderDescription,
                        MRH.MovementRequestHeaderStatus,
                        MRH.MovementRequestHeaderCreatedAt,
                        MRH.MovementRequestHeaderAuthorizedAt,

                        -- MovementType
                        MT.MovementTypeNumber,
                        MT.MovementTypeName,

                        -- Inventory
                        L.iLocation,
                        L.LocationNumber,
                        L.LocationName,

                        -- Product
                        P.iProduct,
                        P.ProductCode,
                        P.ProductDescription

                    FROM MovementRequestDetails AS MRD
                        INNER JOIN MovementRequestHeaders AS MRH ON MRD.iMovementRequestHeader = MRH.iMovementRequestHeader
                        INNER JOIN MovementTypes AS MT ON MRD.iMovementType = MT.iMovementType
                        INNER JOIN Inventories AS I ON MRD.iInventory = I.iInventory
                        INNER JOIN Locations AS L ON I.iLocation = L.iLocation
                        INNER JOIN Products AS P ON I.iProduct = P.iProduct
                    ORDER BY MRH.MovementRequestHeaderNumber DESC;
                ";

                using (var connection = InventoryConnection.GetConnection())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                details.Add(new MovementRequestDetail()
                                {
                                    iMovementRequestDetail = reader.GetGuid(reader.GetOrdinal("iMovementRequestDetail")),
                                    iMovementRequestHeader = reader.GetGuid(reader.GetOrdinal("iMovementRequestHeader")),
                                    iMovementType = reader.GetGuid(reader.GetOrdinal("iMovementType")),
                                    iInventory = reader.GetGuid(reader.GetOrdinal("iInventory")),
                                    MovementRequestDetailDescription = reader.IsDBNull(reader.GetOrdinal("MovementRequestDetailDescription"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("MovementRequestDetailDescription")),
                                    MovementRequestDetailQuantity = reader.GetInt32(reader.GetOrdinal("MovementRequestDetailQuantity")),
                                    MovementRequestDetailStatus = reader.GetInt16(reader.GetOrdinal("MovementRequestDetailStatus")),

                                    // Header
                                    MovementRequestHeaderNumber = reader.GetInt32(reader.GetOrdinal("MovementRequestHeaderNumber")),
                                    MovementRequestHeaderDescription = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderDescription"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("MovementRequestHeaderDescription")),
                                    MovementRequestHeaderStatus = reader.GetInt16(reader.GetOrdinal("MovementRequestHeaderStatus")),
                                    MovementRequestHeaderCreatedAt = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderCreatedAt"))
                                        ? null
                                        : reader.GetDateTime(reader.GetOrdinal("MovementRequestHeaderCreatedAt")),
                                    MovementRequestHeaderAuthorizedAt = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderAuthorizedAt"))
                                        ? null
                                        : reader.GetDateTime(reader.GetOrdinal("MovementRequestHeaderAuthorizedAt")),

                                    // MovementType
                                    MovementTypeNumber = reader.GetInt16(reader.GetOrdinal("MovementTypeNumber")),
                                    MovementTypeName = reader.GetString(reader.GetOrdinal("MovementTypeName")),

                                    // Location
                                    iLocation = reader.GetGuid(reader.GetOrdinal("iLocation")),
                                    LocationNumber = reader.GetInt32(reader.GetOrdinal("LocationNumber")),
                                    LocationName = reader.GetString(reader.GetOrdinal("LocationName")),

                                    // Product
                                    iProduct = reader.GetGuid(reader.GetOrdinal("iProduct")),
                                    ProductCode = reader.GetString(reader.GetOrdinal("ProductCode")),
                                    ProductDescription = reader.GetString(reader.GetOrdinal("ProductDescription"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                details = new List<MovementRequestDetail>();
            }

            return details;
        }

        public bool Find(MovementRequestDetail detail)
        {
            try
            {
                string query = @"
                    SELECT
                        MRD.iMovementRequestDetail,
                        MRD.iMovementRequestHeader,
                        MRD.iMovementType,
                        MRD.iInventory,
                        MRD.MovementRequestDetailDescription,
                        MRD.MovementRequestDetailQuantity,
                        MRD.MovementRequestDetailStatus,

                        -- Header
                        MRH.MovementRequestHeaderNumber,
                        MRH.MovementRequestHeaderDescription,
                        MRH.MovementRequestHeaderStatus,
                        MRH.MovementRequestHeaderCreatedAt,
                        MRH.MovementRequestHeaderAuthorizedAt,

                        -- MovementType
                        MT.MovementTypeNumber,
                        MT.MovementTypeName,

                        -- Inventory
                        L.iLocation,
                        L.LocationNumber,
                        L.LocationName,

                        -- Product
                        P.iProduct,
                        P.ProductCode,
                        P.ProductDescription

                    FROM MovementRequestDetails AS MRD
                        INNER JOIN MovementRequestHeaders AS MRH ON MRD.iMovementRequestHeader = MRH.iMovementRequestHeader
                        INNER JOIN MovementTypes AS MT ON MRD.iMovementType = MT.iMovementType
                        INNER JOIN Inventories AS I ON MRD.iInventory = I.iInventory
                        INNER JOIN Locations AS L ON I.iLocation = L.iLocation
                        INNER JOIN Products AS P ON I.iProduct = P.iProduct
                    WHERE MRD.iMovementRequestDetail = @iMovementRequestDetail;
                ";

                using (var connection = InventoryConnection.GetConnection())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iMovementRequestDetail", detail.iMovementRequestDetail);
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                detail.iMovementRequestHeader = reader.GetGuid(reader.GetOrdinal("iMovementRequestHeader"));
                                detail.iMovementType = reader.GetGuid(reader.GetOrdinal("iMovementType"));
                                detail.iInventory = reader.GetGuid(reader.GetOrdinal("iInventory"));
                                detail.MovementRequestDetailDescription = reader.IsDBNull(reader.GetOrdinal("MovementRequestDetailDescription"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("MovementRequestDetailDescription"));
                                detail.MovementRequestDetailQuantity = reader.GetInt32(reader.GetOrdinal("MovementRequestDetailQuantity"));
                                detail.MovementRequestDetailStatus = reader.GetInt16(reader.GetOrdinal("MovementRequestDetailStatus"));

                                // Header
                                detail.MovementRequestHeaderNumber = reader.GetInt32(reader.GetOrdinal("MovementRequestHeaderNumber"));
                                detail.MovementRequestHeaderDescription = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderDescription"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("MovementRequestHeaderDescription"));
                                detail.MovementRequestHeaderStatus = reader.GetInt16(reader.GetOrdinal("MovementRequestHeaderStatus"));
                                detail.MovementRequestHeaderCreatedAt = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderCreatedAt"))
                                    ? null
                                    : reader.GetDateTime(reader.GetOrdinal("MovementRequestHeaderCreatedAt"));
                                detail.MovementRequestHeaderAuthorizedAt = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderAuthorizedAt"))
                                    ? null
                                    : reader.GetDateTime(reader.GetOrdinal("MovementRequestHeaderAuthorizedAt"));

                                // MovementType
                                detail.MovementTypeNumber = reader.GetInt16(reader.GetOrdinal("MovementTypeNumber"));
                                detail.MovementTypeName = reader.GetString(reader.GetOrdinal("MovementTypeName"));

                                // Location
                                detail.iLocation = reader.GetGuid(reader.GetOrdinal("iLocation"));
                                detail.LocationNumber = reader.GetInt32(reader.GetOrdinal("LocationNumber"));
                                detail.LocationName = reader.GetString(reader.GetOrdinal("LocationName"));

                                // Product
                                detail.iProduct = reader.GetGuid(reader.GetOrdinal("iProduct"));
                                detail.ProductCode = reader.GetString(reader.GetOrdinal("ProductCode"));
                                detail.ProductDescription = reader.GetString(reader.GetOrdinal("ProductDescription"));

                                return true;
                            }
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

        public bool Insert(MovementRequestDetail detail)
        {
            try
            {
                string query = @"
                    INSERT INTO MovementRequestDetails (
                        iMovementRequestDetail,
                        iMovementRequestHeader,
                        iMovementType,
                        iInventory,
                        MovementRequestDetailDescription,
                        MovementRequestDetailQuantity,
                        MovementRequestDetailStatus
                    )
                    VALUES (
                        @iMovementRequestDetail,
                        @iMovementRequestHeader,
                        @iMovementType,
                        @iInventory,
                        @MovementRequestDetailDescription,
                        @MovementRequestDetailQuantity,
                        @MovementRequestDetailStatus
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iMovementRequestDetail", detail.iMovementRequestDetail);
                        insertCommand.Parameters.AddWithValue("@iMovementRequestHeader", detail.iMovementRequestHeader);
                        insertCommand.Parameters.AddWithValue("@iMovementType", detail.iMovementType);
                        insertCommand.Parameters.AddWithValue("@iInventory", detail.iInventory);
                        insertCommand.Parameters.AddWithValue("@MovementRequestDetailDescription", (object?)detail.MovementRequestDetailDescription ?? DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@MovementRequestDetailQuantity", detail.MovementRequestDetailQuantity);
                        insertCommand.Parameters.AddWithValue("@MovementRequestDetailStatus", detail.MovementRequestDetailStatus);
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

        public bool Update(MovementRequestDetail detail)
        {
            try
            {
                string query = @"
                    UPDATE MovementRequestDetails
                    SET
                        iMovementRequestHeader = @iMovementRequestHeader,
                        iMovementType = @iMovementType,
                        iInventory = @iInventory,
                        MovementRequestDetailDescription = @MovementRequestDetailDescription,
                        MovementRequestDetailQuantity = @MovementRequestDetailQuantity,
                        MovementRequestDetailStatus = @MovementRequestDetailStatus
                    WHERE iMovementRequestDetail = @iMovementRequestDetail;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@iMovementRequestDetail", detail.iMovementRequestDetail);
                        updateCommand.Parameters.AddWithValue("@iMovementRequestHeader", detail.iMovementRequestHeader);
                        updateCommand.Parameters.AddWithValue("@iMovementType", detail.iMovementType);
                        updateCommand.Parameters.AddWithValue("@iInventory", detail.iInventory);
                        updateCommand.Parameters.AddWithValue("@MovementRequestDetailDescription", (object?)detail.MovementRequestDetailDescription ?? DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@MovementRequestDetailQuantity", detail.MovementRequestDetailQuantity);
                        updateCommand.Parameters.AddWithValue("@MovementRequestDetailStatus", detail.MovementRequestDetailStatus);
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

        public bool Delete(Guid movementRequestDetailId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM MovementRequestDetails
                    WHERE iMovementRequestDetail = @iMovementRequestDetail;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@iMovementRequestDetail", movementRequestDetailId);
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
