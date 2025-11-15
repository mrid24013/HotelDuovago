using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class MovementRepository
    {
        public List<Movement> GetAll()
        {
            List<Movement> movements = new List<Movement>();
            try
            {
                string query = @"
                    SELECT
                        M.iMovement,
                        M.MovementQuantity,
                        M.MovementCreatedAt,

                        -- Product
                        P.iProduct,
                        P.ProductCode,
                        P.ProductDescription,
                        P.ProductPrice,
                        P.iManufacturer,

                        -- FromInventory
                        FI.iInventory AS FromInventory_iInventory,
                        FIL.iLocation AS FromInventoryLocation_iLocation,
                        FIL.LocationNumber AS FromInventoryLocation_LocationNumber,
                        FIL.LocationName AS FromInventoryLocation_LocationName,

                        -- ToInventory
                        TI.iInventory AS ToInventory_iInventory,
                        TIL.iLocation AS ToInventoryLocation_iLocation,
                        TIL.LocationNumber AS ToInventoryLocation_LocationNumber,
                        TIL.LocationName AS ToInventoryLocation_LocationName,

                        -- MovementType
                        MT.iMovementType,
                        MT.MovementTypeNumber,
                        MT.MovementTypeName,

                        -- MovementRequest
                        MRD.iMovementRequestDetail,
                        MRH.iMovementRequestHeader,
                        MRH.MovementRequestHeaderNumber

                    FROM Movements AS M
                        LEFT JOIN Products AS P ON M.iProduct = P.iProduct
                        LEFT JOIN Inventories AS FI ON M.iFromInventory = FI.iInventory
                        LEFT JOIN Locations AS FIL ON FI.iLocation = FIL.iLocation
                        LEFT JOIN Inventories AS TI ON M.iToInventory = TI.iInventory
                        LEFT JOIN Locations AS TIL ON TI.iLocation = TIL.iLocation
                        LEFT JOIN MovementTypes AS MT ON M.iMovementType = MT.iMovementType
                        LEFT JOIN MovementRequestDetails AS MRD ON M.iMovementRequestDetail = MRD.iMovementRequestDetail
                        LEFT JOIN MovementRequestHeaders AS MRH ON MRD.iMovementRequestHeader = MRH.iMovementRequestHeader
                    ORDER BY M.MovementCreatedAt DESC;
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
                                movements.Add(new Movement()
                                {
                                    iMovement = reader.GetGuid(reader.GetOrdinal("iMovement")),
                                    MovementQuantity = reader.GetInt32(reader.GetOrdinal("MovementQuantity")),
                                    MovementCreatedAt = reader.GetDateTime(reader.GetOrdinal("MovementCreatedAt")),

                                    // JOIN Product
                                    iProduct = reader.GetGuid(reader.GetOrdinal("iProduct")),
                                    ProductCode = reader.GetString(reader.GetOrdinal("ProductCode")),
                                    ProductDescription = reader.IsDBNull(reader.GetOrdinal("ProductDescription"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("ProductDescription")),
                                    ProductPrice = reader.IsDBNull(reader.GetOrdinal("ProductPrice"))
                                        ? null
                                        : reader.GetDecimal(reader.GetOrdinal("ProductPrice")),
                                    iManufacturer = reader.IsDBNull(reader.GetOrdinal("iManufacturer"))
                                        ? null
                                        : reader.GetGuid(reader.GetOrdinal("iManufacturer")),

                                    // JOIN FromInventory
                                    iFromInventory = reader.IsDBNull(reader.GetOrdinal("FromInventory_iInventory"))
                                        ? null
                                        : reader.GetGuid(reader.GetOrdinal("FromInventory_iInventory")),
                                    iFromInventoryLocation = reader.IsDBNull(reader.GetOrdinal("FromInventoryLocation_iLocation"))
                                        ? null
                                        : reader.GetGuid(reader.GetOrdinal("FromInventoryLocation_iLocation")),
                                    FromInventoryLocationNumber = reader.IsDBNull(reader.GetOrdinal("FromInventoryLocation_LocationNumber"))
                                        ? null
                                        : reader.GetInt32(reader.GetOrdinal("FromInventoryLocation_LocationNumber")),
                                    FromInventoryLocationName = reader.IsDBNull(reader.GetOrdinal("FromInventoryLocation_LocationName"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("FromInventoryLocation_LocationName")),

                                    // JOIN ToInventory
                                    iToInventory = reader.IsDBNull(reader.GetOrdinal("ToInventory_iInventory"))
                                        ? null
                                        : reader.GetGuid(reader.GetOrdinal("ToInventory_iInventory")),
                                    iToInventoryLocation = reader.IsDBNull(reader.GetOrdinal("ToInventoryLocation_iLocation"))
                                        ? null
                                        : reader.GetGuid(reader.GetOrdinal("ToInventoryLocation_iLocation")),
                                    ToInventoryLocationNumber = reader.IsDBNull(reader.GetOrdinal("ToInventoryLocation_LocationNumber"))
                                        ? null
                                        : reader.GetInt32(reader.GetOrdinal("ToInventoryLocation_LocationNumber")),
                                    ToInventoryLocationName = reader.IsDBNull(reader.GetOrdinal("ToInventoryLocation_LocationName"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("ToInventoryLocation_LocationName")),

                                    // JOIN MovementType
                                    iMovementType = reader.GetGuid(reader.GetOrdinal("iMovementType")),
                                    MovementTypeNumber = reader.GetInt16(reader.GetOrdinal("MovementTypeNumber")),
                                    MovementTypeName = reader.GetString(reader.GetOrdinal("MovementTypeName")),

                                    // JOIN MovementRequestDetail
                                    iMovementRequestDetail = reader.IsDBNull(reader.GetOrdinal("iMovementRequestDetail"))
                                        ? null
                                        : reader.GetGuid(reader.GetOrdinal("iMovementRequestDetail")),

                                    // JOIN MovementRequestHeader
                                    iMovementRequestHeader = reader.IsDBNull(reader.GetOrdinal("iMovementRequestHeader"))
                                        ? null
                                        : reader.GetGuid(reader.GetOrdinal("iMovementRequestHeader")),
                                    MovementRequestHeaderNumber = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderNumber"))
                                        ? null
                                        : reader.GetInt32(reader.GetOrdinal("MovementRequestHeaderNumber"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                movements = new List<Movement>();
            }

            return movements;
        }

        public bool Find(Movement movement)
        {
            try
            {
                string query = @"
                    SELECT
                        M.iMovement,
                        M.MovementQuantity,
                        M.MovementCreatedAt,

                        -- Product
                        P.iProduct,
                        P.ProductCode,
                        P.ProductDescription,
                        P.ProductPrice,
                        P.iManufacturer,

                        -- FromInventory
                        FI.iInventory AS FromInventory_iInventory,
                        FIL.iLocation AS FromInventoryLocation_iLocation,
                        FIL.LocationNumber AS FromInventoryLocation_LocationNumber,
                        FIL.LocationName AS FromInventoryLocation_LocationName,

                        -- ToInventory
                        TI.iInventory AS ToInventory_iInventory,
                        TIL.iLocation AS ToInventoryLocation_iLocation,
                        TIL.LocationNumber AS ToInventoryLocation_LocationNumber,
                        TIL.LocationName AS ToInventoryLocation_LocationName,

                        -- MovementType
                        MT.iMovementType,
                        MT.MovementTypeNumber,
                        MT.MovementTypeName,

                        -- MovementRequest
                        MRD.iMovementRequestDetail,
                        MRH.iMovementRequestHeader,
                        MRH.MovementRequestHeaderNumber

                    FROM Movements AS M
                        LEFT JOIN Products AS P ON M.iProduct = P.iProduct
                        LEFT JOIN Inventories AS FI ON M.iFromInventory = FI.iInventory
                        LEFT JOIN Locations AS FIL ON FI.iLocation = FIL.iLocation
                        LEFT JOIN Inventories AS TI ON M.iToInventory = TI.iInventory
                        LEFT JOIN Locations AS TIL ON TI.iLocation = TIL.iLocation
                        LEFT JOIN MovementTypes AS MT ON M.iMovementType = MT.iMovementType
                        LEFT JOIN MovementRequestDetails AS MRD ON M.iMovementRequestDetail = MRD.iMovementRequestDetail
                        LEFT JOIN MovementRequestHeaders AS MRH ON MRD.iMovementRequestHeader = MRH.iMovementRequestHeader
                    WHERE M.iMovement = @iMovement;
                ";

                using (var connection = InventoryConnection.GetConnection())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iMovement", movement.iMovement);
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                movement.iMovement = reader.GetGuid(reader.GetOrdinal("iMovement"));
                                movement.MovementQuantity = reader.GetInt32(reader.GetOrdinal("MovementQuantity"));
                                movement.MovementCreatedAt = reader.GetDateTime(reader.GetOrdinal("MovementCreatedAt"));

                                // JOIN Product
                                movement.iProduct = reader.GetGuid(reader.GetOrdinal("iProduct"));
                                movement.ProductCode = reader.GetString(reader.GetOrdinal("ProductCode"));
                                movement.ProductDescription = reader.IsDBNull(reader.GetOrdinal("ProductDescription"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("ProductDescription"));
                                movement.ProductPrice = reader.IsDBNull(reader.GetOrdinal("ProductPrice"))
                                    ? null
                                    : reader.GetDecimal(reader.GetOrdinal("ProductPrice"));
                                movement.iManufacturer = reader.IsDBNull(reader.GetOrdinal("iManufacturer"))
                                    ? null
                                    : reader.GetGuid(reader.GetOrdinal("iManufacturer"));

                                // JOIN FromInventory
                                movement.iFromInventory = reader.IsDBNull(reader.GetOrdinal("FromInventory_iInventory"))
                                    ? null
                                    : reader.GetGuid(reader.GetOrdinal("FromInventory_iInventory"));
                                movement.iFromInventoryLocation = reader.IsDBNull(reader.GetOrdinal("FromInventoryLocation_iLocation"))
                                    ? null
                                    : reader.GetGuid(reader.GetOrdinal("FromInventoryLocation_iLocation"));
                                movement.FromInventoryLocationNumber = reader.IsDBNull(reader.GetOrdinal("FromInventoryLocation_LocationNumber"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("FromInventoryLocation_LocationNumber"));
                                movement.FromInventoryLocationName = reader.IsDBNull(reader.GetOrdinal("FromInventoryLocation_LocationName"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("FromInventoryLocation_LocationName"));

                                // JOIN ToInventory
                                movement.iToInventory = reader.IsDBNull(reader.GetOrdinal("ToInventory_iInventory"))
                                    ? null
                                    : reader.GetGuid(reader.GetOrdinal("ToInventory_iInventory"));
                                movement.iToInventoryLocation = reader.IsDBNull(reader.GetOrdinal("ToInventoryLocation_iLocation"))
                                    ? null
                                    : reader.GetGuid(reader.GetOrdinal("ToInventoryLocation_iLocation"));
                                movement.ToInventoryLocationNumber = reader.IsDBNull(reader.GetOrdinal("ToInventoryLocation_LocationNumber"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("ToInventoryLocation_LocationNumber"));
                                movement.ToInventoryLocationName = reader.IsDBNull(reader.GetOrdinal("ToInventoryLocation_LocationName"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("ToInventoryLocation_LocationName"));

                                // JOIN MovementType
                                movement.iMovementType = reader.GetGuid(reader.GetOrdinal("iMovementType"));
                                movement.MovementTypeNumber = reader.GetInt16(reader.GetOrdinal("MovementTypeNumber"));
                                movement.MovementTypeName = reader.GetString(reader.GetOrdinal("MovementTypeName"));

                                // JOIN MovementRequestDetail
                                movement.iMovementRequestDetail = reader.IsDBNull(reader.GetOrdinal("iMovementRequestDetail"))
                                    ? null
                                    : reader.GetGuid(reader.GetOrdinal("iMovementRequestDetail"));

                                // JOIN MovementRequestHeader
                                movement.iMovementRequestHeader = reader.IsDBNull(reader.GetOrdinal("iMovementRequestHeader"))
                                    ? null
                                    : reader.GetGuid(reader.GetOrdinal("iMovementRequestHeader"));
                                movement.MovementRequestHeaderNumber = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderNumber"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("MovementRequestHeaderNumber"));
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

        public bool Insert(Movement movement)
        {
            try
            {
                string query = @"
                    INSERT INTO Movements (
                        iMovement,
                        MovementQuantity,
                        MovementCreatedAt,
                        iProduct,
                        iFromInventory,
                        iToInventory,
                        iMovementType,
                        iMovementRequestDetail
                    )
                    VALUES (
                        @iMovement,
                        @MovementQuantity,
                        @MovementCreatedAt,
                        @iProduct,
                        @iFromInventory,
                        @iToInventory,
                        @iMovementType,
                        @iMovementRequestDetail
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iMovement", movement.iMovement);
                        insertCommand.Parameters.AddWithValue("@MovementQuantity", movement.MovementQuantity);
                        insertCommand.Parameters.AddWithValue("@MovementCreatedAt", movement.MovementCreatedAt);
                        insertCommand.Parameters.AddWithValue("@iProduct", (object?)movement.iProduct ?? DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@iFromInventory", (object?)movement.iFromInventory ?? DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@iToInventory", (object?)movement.iToInventory ?? DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@iMovementType", (object?)movement.iMovementType ?? DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@iMovementRequestDetail", (object?)movement.iMovementRequestDetail ?? DBNull.Value);
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

        public bool Delete(Guid movementId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM Movements
                    WHERE iMovement = @iMovement;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@iMovement", movementId);
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
