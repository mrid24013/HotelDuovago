using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class ProductRepository
    {
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>() { };
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        P.iProduct,
                        P.ProductCode,
                        P.ProductDescription,
                        P.ProductPrice,
                        P.iManufacturer,
                        M.ManufacturerCode,
                        M.ManufacturerName
                    FROM Products P
                    INNER JOIN Manufacturers M ON P.iManufacturer = M.iManufacturer
                    ORDER BY P.ProductCode ASC;
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
                                products.Add(new Product()
                                {
                                    iProduct = reader.GetGuid(reader.GetOrdinal("iProduct")),
                                    ProductCode = reader.GetString(reader.GetOrdinal("ProductCode")),
                                    ProductDescription = reader.GetString(reader.GetOrdinal("ProductDescription")),
                                    ProductPrice = reader.GetDecimal(reader.GetOrdinal("ProductPrice")),
                                    iManufacturer = reader.GetGuid(reader.GetOrdinal("iManufacturer")),
                                    ManufacturerCode = reader.GetString(reader.GetOrdinal("ManufacturerCode")),
                                    ManufacturerName = reader.GetString(reader.GetOrdinal("ManufacturerName")),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                products = new List<Product>() { };
            }

            return products;
        }

        public bool Find(Product product)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        P.iProduct,
                        P.ProductCode,
                        P.ProductDescription,
                        P.ProductPrice,
                        P.iManufacturer,
                        M.ManufacturerCode,
                        M.ManufacturerName
                    FROM Products P
                    INNER JOIN Manufacturers M ON P.iManufacturer = M.iManufacturer
                    WHERE P.iProduct = @iProduct;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iProduct", product.iProduct);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                product.ProductCode = reader.GetString(reader.GetOrdinal("ProductCode"));
                                product.ProductDescription = reader.GetString(reader.GetOrdinal("ProductDescription"));
                                product.ProductPrice = reader.GetDecimal(reader.GetOrdinal("ProductPrice"));
                                product.iManufacturer = reader.GetGuid(reader.GetOrdinal("iManufacturer"));
                                product.ManufacturerCode = reader.GetString(reader.GetOrdinal("ManufacturerCode"));
                                product.ManufacturerName = reader.GetString(reader.GetOrdinal("ManufacturerName"));
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

        public bool Insert(Product product)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO Products (
                        iProduct,
                        ProductCode,
                        ProductDescription,
                        ProductPrice,
                        iManufacturer
                    )
                    VALUES (
                        @iProduct,
                        @ProductCode,
                        @ProductDescription,
                        @ProductPrice,
                        @iManufacturer
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iProduct", product.iProduct);
                        insertCommand.Parameters.AddWithValue("@ProductCode", product.ProductCode);
                        insertCommand.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                        insertCommand.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                        insertCommand.Parameters.AddWithValue("@iManufacturer", product.iManufacturer);
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

        public bool Update(Product product)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    UPDATE Products
                    SET
                        ProductCode = @ProductCode,
                        ProductDescription = @ProductDescription,
                        ProductPrice = @ProductPrice,
                        iManufacturer = @iManufacturer
                    WHERE iProduct = @iProduct;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@iProduct", product.iProduct);
                        updateCommand.Parameters.AddWithValue("@ProductCode", product.ProductCode);
                        updateCommand.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                        updateCommand.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                        updateCommand.Parameters.AddWithValue("@iManufacturer", product.iManufacturer);
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

        public bool Delete(Guid productId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM Products
                    WHERE iProduct = @iProduct;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@iProduct", productId);
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
