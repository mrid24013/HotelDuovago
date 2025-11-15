using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class InventoryRepository
    {
        public List<Inventory> GetAll()
        {
            List<Inventory> inventories = new List<Inventory>() { };
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        i.iInventory,
                        i.InventoryStock,
                        i.InventoryMinStock,
                        i.InventoryMaxStock,

                        -- Location
                        l.iLocation,
                        l.LocationNumber,
                        l.LocationName,
                        l.LocationDescription,
                        l.LocationEnabled,

                        -- Product
                        p.iProduct,
                        p.ProductCode,
                        p.ProductDescription,
                        p.ProductPrice,
                        p.iManufacturer,

                        -- Created By (User)
                        u.iUser,
                        u.UserNumber,
                        u.UserFullName,
                        u.UserEmail
                    FROM Inventories i
                    INNER JOIN Locations l ON i.iLocation = l.iLocation
                    INNER JOIN Products p ON i.iProduct = p.iProduct
                    INNER JOIN Users u ON i.iCreatedBy = u.iUser;
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
                                inventories.Add(new Inventory()
                                {
                                    iInventory = reader.GetGuid(reader.GetOrdinal("iInventory")),
                                    InventoryStock = reader.GetInt32(reader.GetOrdinal("InventoryStock")),
                                    InventoryMinStock = reader.GetInt32(reader.GetOrdinal("InventoryMinStock")),
                                    InventoryMaxStock = reader.GetInt32(reader.GetOrdinal("InventoryMaxStock")),

                                    // Location
                                    iLocation = reader.GetGuid(reader.GetOrdinal("iLocation")),
                                    LocationNumber = reader.GetInt32(reader.GetOrdinal("LocationNumber")),
                                    LocationName = reader.GetString(reader.GetOrdinal("LocationName")),
                                    LocationDescription = reader.IsDBNull(reader.GetOrdinal("LocationDescription"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("LocationDescription")),
                                    LocationEnabled = reader.GetBoolean(reader.GetOrdinal("LocationEnabled")),

                                    // Product
                                    iProduct = reader.GetGuid(reader.GetOrdinal("iProduct")),
                                    ProductCode = reader.GetString(reader.GetOrdinal("ProductCode")),
                                    ProductDescription = reader.GetString(reader.GetOrdinal("ProductDescription")),
                                    ProductPrice = reader.GetDecimal(reader.GetOrdinal("ProductPrice")),
                                    iManufacturer = reader.GetGuid(reader.GetOrdinal("iManufacturer")),

                                    // Created By
                                    iCreatedBy = reader.GetGuid(reader.GetOrdinal("iUser")),
                                    CreatorNumber = reader.GetInt32(reader.GetOrdinal("UserNumber")),
                                    CreatorFullName = reader.IsDBNull(reader.GetOrdinal("UserFullName"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("UserFullName")),
                                    CreatorEmail = reader.IsDBNull(reader.GetOrdinal("UserEmail"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("UserEmail")),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                inventories = new List<Inventory>() { };
            }

            return inventories;
        }

        public bool Find(Inventory inventory)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        i.iInventory,
                        i.InventoryStock,
                        i.InventoryMinStock,
                        i.InventoryMaxStock,

                        -- Location
                        l.iLocation,
                        l.LocationNumber,
                        l.LocationName,
                        l.LocationDescription,
                        l.LocationEnabled,

                        -- Product
                        p.iProduct,
                        p.ProductCode,
                        p.ProductDescription,
                        p.ProductPrice,
                        p.iManufacturer,

                        -- Created By (User)
                        u.iUser,
                        u.UserNumber,
                        u.UserFullName,
                        u.UserEmail
                    FROM Inventories i
                    INNER JOIN Locations l ON i.iLocation = l.iLocation
                    INNER JOIN Products p ON i.iProduct = p.iProduct
                    INNER JOIN Users u ON i.iCreatedBy = u.iUser
                    WHERE i.iInventory = @InventoryId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InventoryId", inventory.iInventory);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                inventory.iInventory = reader.GetGuid(reader.GetOrdinal("iInventory"));
                                inventory.InventoryStock = reader.GetInt32(reader.GetOrdinal("InventoryStock"));
                                inventory.InventoryMinStock = reader.GetInt32(reader.GetOrdinal("InventoryMinStock"));
                                inventory.InventoryMaxStock = reader.GetInt32(reader.GetOrdinal("InventoryMaxStock"));

                                // Location
                                inventory.iLocation = reader.GetGuid(reader.GetOrdinal("iLocation"));
                                inventory.LocationNumber = reader.GetInt32(reader.GetOrdinal("LocationNumber"));
                                inventory.LocationName = reader.GetString(reader.GetOrdinal("LocationName"));
                                inventory.LocationDescription = reader.IsDBNull(reader.GetOrdinal("LocationDescription"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("LocationDescription"));
                                inventory.LocationEnabled = reader.GetBoolean(reader.GetOrdinal("LocationEnabled"));

                                // Product
                                inventory.iProduct = reader.GetGuid(reader.GetOrdinal("iProduct"));
                                inventory.ProductCode = reader.GetString(reader.GetOrdinal("ProductCode"));
                                inventory.ProductDescription = reader.GetString(reader.GetOrdinal("ProductDescription"));
                                inventory.ProductPrice = reader.GetDecimal(reader.GetOrdinal("ProductPrice"));
                                inventory.iManufacturer = reader.GetGuid(reader.GetOrdinal("iManufacturer"));

                                // Created By
                                inventory.iCreatedBy = reader.GetGuid(reader.GetOrdinal("iUser"));
                                inventory.CreatorNumber = reader.GetInt32(reader.GetOrdinal("UserNumber"));
                                inventory.CreatorFullName = reader.IsDBNull(reader.GetOrdinal("UserFullName"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("UserFullName"));
                                inventory.CreatorEmail = reader.IsDBNull(reader.GetOrdinal("UserEmail"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("UserEmail"));
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

        public bool Insert(Inventory inventory)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO Inventories (
                        iInventory,
                        iLocation,
                        iProduct,
                        iCreatedBy,
                        InventoryStock,
                        InventoryMinStock,
                        InventoryMaxStock
                    )
                    VALUES (
                        @iInventory,
                        @iLocation,
                        @iProduct,
                        @iCreatedBy,
                        @InventoryStock,
                        @InventoryMinStock,
                        @InventoryMaxStock
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iInventory", inventory.iInventory);
                        insertCommand.Parameters.AddWithValue("@iLocation", inventory.iLocation);
                        insertCommand.Parameters.AddWithValue("@iProduct", inventory.iProduct);
                        insertCommand.Parameters.AddWithValue("@iCreatedBy", inventory.iCreatedBy);
                        insertCommand.Parameters.AddWithValue("@InventoryStock", inventory.InventoryStock);
                        insertCommand.Parameters.AddWithValue("@InventoryMinStock", inventory.InventoryMinStock);
                        insertCommand.Parameters.AddWithValue("@InventoryMaxStock", inventory.InventoryMaxStock);
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

        public bool Update(Inventory inventory)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    UPDATE Inventories
                    SET 
                        iLocation = @iLocation,
                        iProduct = @iProduct,
                        iCreatedBy = @iCreatedBy,
                        InventoryStock = @InventoryStock,
                        InventoryMinStock = @InventoryMinStock,
                        InventoryMaxStock = @InventoryMaxStock
                    WHERE iInventory = @iInventory;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@iInventory", inventory.iInventory);
                        updateCommand.Parameters.AddWithValue("@iLocation", inventory.iLocation);
                        updateCommand.Parameters.AddWithValue("@iProduct", inventory.iProduct);
                        updateCommand.Parameters.AddWithValue("@iCreatedBy", inventory.iCreatedBy);
                        updateCommand.Parameters.AddWithValue("@InventoryStock", inventory.InventoryStock);
                        updateCommand.Parameters.AddWithValue("@InventoryMinStock", inventory.InventoryMinStock);
                        updateCommand.Parameters.AddWithValue("@InventoryMaxStock", inventory.InventoryMaxStock);
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

        public bool Delete(Guid inventoryId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM Inventories
                    WHERE iInventory = @InventoryId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@InventoryId", inventoryId);
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
