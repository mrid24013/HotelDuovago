using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class MovementTypeRepository
    {
        public List<MovementType> GetAll()
        {
            List<MovementType> movementTypes = new List<MovementType>();
            try
            {
                string query = @"
                    SELECT
                        iMovementType,
                        MovementTypeNumber,
                        MovementTypeName,
                        MovementTypeDescription
                    FROM MovementTypes
                    ORDER BY MovementTypeNumber ASC;
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
                                movementTypes.Add(new MovementType()
                                {
                                    iMovementType = reader.GetGuid(reader.GetOrdinal("iMovementType")),
                                    MovementTypeNumber = reader.GetInt16(reader.GetOrdinal("MovementTypeNumber")),
                                    MovementTypeName = reader.GetString(reader.GetOrdinal("MovementTypeName")),
                                    MovementTypeDescription = reader.GetString(reader.GetOrdinal("MovementTypeDescription"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                movementTypes = new List<MovementType>();
            }

            return movementTypes;
        }

        public bool Find(MovementType movementType)
        {
            try
            {
                string query = @"
                    SELECT
                        iMovementType,
                        MovementTypeNumber,
                        MovementTypeName,
                        MovementTypeDescription
                    FROM MovementTypes
                    WHERE iMovementType = @iMovementType;
                ";

                using (var connection = InventoryConnection.GetConnection())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iMovementType", movementType.iMovementType);
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                movementType.iMovementType = reader.GetGuid(reader.GetOrdinal("iMovementType"));
                                movementType.MovementTypeNumber = reader.GetInt16(reader.GetOrdinal("MovementTypeNumber"));
                                movementType.MovementTypeName = reader.GetString(reader.GetOrdinal("MovementTypeName"));
                                movementType.MovementTypeDescription = reader.GetString(reader.GetOrdinal("MovementTypeDescription"));
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

        public bool Insert(MovementType movementType)
        {
            try
            {
                string query = @"
                    INSERT INTO MovementTypes (
                        iMovementType,
                        MovementTypeNumber,
                        MovementTypeName,
                        MovementTypeDescription
                    )
                    VALUES (
                        @iMovementType,
                        @MovementTypeNumber,
                        @MovementTypeName,
                        @MovementTypeDescription
                    );
                ";

                using (var connection = InventoryConnection.GetConnection())
                {
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iMovementType", movementType.iMovementType);
                        insertCommand.Parameters.AddWithValue("@MovementTypeNumber", movementType.MovementTypeNumber);
                        insertCommand.Parameters.AddWithValue("@MovementTypeName", movementType.MovementTypeName);
                        insertCommand.Parameters.AddWithValue("@MovementTypeDescription", movementType.MovementTypeDescription);
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

        public bool Update(MovementType movementType)
        {
            try
            {
                string query = @"
                    UPDATE MovementTypes
                    SET
                        MovementTypeNumber = @MovementTypeNumber,
                        MovementTypeName = @MovementTypeName,
                        MovementTypeDescription = @MovementTypeDescription
                    WHERE iMovementType = @iMovementType;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@iMovementType", movementType.iMovementType);
                        updateCommand.Parameters.AddWithValue("@MovementTypeNumber", movementType.MovementTypeNumber);
                        updateCommand.Parameters.AddWithValue("@MovementTypeName", movementType.MovementTypeName);
                        updateCommand.Parameters.AddWithValue("@MovementTypeDescription", movementType.MovementTypeDescription);
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

        public bool Delete(Guid movementTypeId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM MovementTypes
                    WHERE iMovementType = @iMovementType;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@iMovementType", movementTypeId);
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

