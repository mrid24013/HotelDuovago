using System.Data;

using DataAccess.Connections;
using DataAccess.Models;

using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class MovementRequestHeaderRepository
    {
        public List<MovementRequestHeader> GetAll()
        {
            List<MovementRequestHeader> headers = new List<MovementRequestHeader>();
            try
            {
                string query = @"
                    SELECT
                        MRH.iMovementRequestHeader,
                        MRH.iRequestedBy,
                        MRH.iAuthorizedBy,
                        MRH.MovementRequestHeaderNumber,
                        MRH.MovementRequestHeaderDescription,
                        MRH.MovementRequestHeaderStatus,
                        MRH.MovementRequestHeaderCreatedAt,
                        MRH.MovementRequestHeaderAuthorizedAt,

                        -- RequestedBy
                        RB.UserNumber AS RequestedBy_UserNumber,
                        RB.UserFullName AS RequestedBy_UserFullName,
                        RB.UserEmail AS RequestedBy_UserEmail,

                        -- AuthorizedBy (puede ser nulo)
                        AB.UserNumber AS AuthorizedBy_UserNumber,
                        AB.UserFullName AS AuthorizedBy_UserFullName,
                        AB.UserEmail AS AuthorizedBy_UserEmail

                    FROM MovementRequestHeaders AS MRH
                        INNER JOIN Users AS RB ON MRH.iRequestedBy = RB.iUser
                        LEFT JOIN Users AS AB ON MRH.iAuthorizedBy = AB.iUser
                    ORDER BY MRH.MovementRequestHeaderCreatedAt DESC;
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
                                headers.Add(new MovementRequestHeader()
                                {
                                    iMovementRequestHeader = reader.GetGuid(reader.GetOrdinal("iMovementRequestHeader")),
                                    iRequestedBy = reader.GetGuid(reader.GetOrdinal("iRequestedBy")),
                                    iAuthorizedBy = reader.GetGuid(reader.GetOrdinal("iAuthorizedBy")),
                                    MovementRequestHeaderNumber = reader.GetInt32(reader.GetOrdinal("MovementRequestHeaderNumber")),
                                    MovementRequestHeaderDescription = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderDescription"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("MovementRequestHeaderDescription")),
                                    MovementRequestHeaderStatus = reader.GetInt16(reader.GetOrdinal("MovementRequestHeaderStatus")),
                                    MovementRequestHeaderCreatedAt = reader.GetDateTime(reader.GetOrdinal("MovementRequestHeaderCreatedAt")),
                                    MovementRequestHeaderAuthorizedAt = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderAuthorizedAt"))
                                        ? null
                                        : reader.GetDateTime(reader.GetOrdinal("MovementRequestHeaderAuthorizedAt")),

                                    // RequestedBy
                                    CreatorNumber = reader.GetInt32(reader.GetOrdinal("RequestedBy_UserNumber")),
                                    CreatorFullName = reader.GetString(reader.GetOrdinal("RequestedBy_UserFullName")),
                                    CreatorEmail = reader.IsDBNull(reader.GetOrdinal("RequestedBy_UserEmail"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("RequestedBy_UserEmail")),

                                    // AuthorizedBy puede ser nulo
                                    AuthorizerNumber = reader.IsDBNull(reader.GetOrdinal("AuthorizedBy_UserNumber"))
                                        ? null
                                        : reader.GetInt32(reader.GetOrdinal("AuthorizedBy_UserNumber")),
                                    AuthorizerFullName = reader.IsDBNull(reader.GetOrdinal("AuthorizedBy_UserFullName"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("AuthorizedBy_UserFullName")),
                                    AuthorizerEmail = reader.IsDBNull(reader.GetOrdinal("AuthorizedBy_UserEmail"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("AuthorizedBy_UserEmail"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                headers = new List<MovementRequestHeader>();
            }

            return headers;
        }

        public bool Find(MovementRequestHeader header)
        {
            try
            {
                string query = @"
                    SELECT
                        MRH.iMovementRequestHeader,
                        MRH.iRequestedBy,
                        MRH.iAuthorizedBy,
                        MRH.MovementRequestHeaderNumber,
                        MRH.MovementRequestHeaderDescription,
                        MRH.MovementRequestHeaderStatus,
                        MRH.MovementRequestHeaderCreatedAt,
                        MRH.MovementRequestHeaderAuthorizedAt,

                        -- RequestedBy
                        RB.UserNumber AS RequestedBy_UserNumber,
                        RB.UserFullName AS RequestedBy_UserFullName,
                        RB.UserEmail AS RequestedBy_UserEmail,

                        -- AuthorizedBy (puede ser nulo)
                        AB.UserNumber AS AuthorizedBy_UserNumber,
                        AB.UserFullName AS AuthorizedBy_UserFullName,
                        AB.UserEmail AS AuthorizedBy_UserEmail

                    FROM MovementRequestHeaders AS MRH
                        INNER JOIN Users AS RB ON MRH.iRequestedBy = RB.iUser
                        LEFT JOIN Users AS AB ON MRH.iAuthorizedBy = AB.iUser
                    WHERE MRH.iMovementRequestHeader = @iMovementRequestHeader;
                ";

                using (var connection = InventoryConnection.GetConnection())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iMovementRequestHeader", header.iMovementRequestHeader);
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                header.iRequestedBy = reader.GetGuid(reader.GetOrdinal("iRequestedBy"));
                                header.iAuthorizedBy = reader.GetGuid(reader.GetOrdinal("iAuthorizedBy"));
                                header.MovementRequestHeaderNumber = reader.GetInt32(reader.GetOrdinal("MovementRequestHeaderNumber"));
                                header.MovementRequestHeaderDescription = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderDescription"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("MovementRequestHeaderDescription"));
                                header.MovementRequestHeaderStatus = reader.GetInt16(reader.GetOrdinal("MovementRequestHeaderStatus"));
                                header.MovementRequestHeaderCreatedAt = reader.GetDateTime(reader.GetOrdinal("MovementRequestHeaderCreatedAt"));
                                header.MovementRequestHeaderAuthorizedAt = reader.IsDBNull(reader.GetOrdinal("MovementRequestHeaderAuthorizedAt"))
                                    ? null
                                    : reader.GetDateTime(reader.GetOrdinal("MovementRequestHeaderAuthorizedAt"));

                                // RequestedBy
                                header.CreatorNumber = reader.GetInt32(reader.GetOrdinal("RequestedBy_UserNumber"));
                                header.CreatorFullName = reader.GetString(reader.GetOrdinal("RequestedBy_UserFullName"));
                                header.CreatorEmail = reader.IsDBNull(reader.GetOrdinal("RequestedBy_UserEmail"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("RequestedBy_UserEmail"));

                                // AuthorizedBy puede ser nulo
                                header.AuthorizerNumber = reader.IsDBNull(reader.GetOrdinal("AuthorizedBy_UserNumber"))
                                    ? null
                                    : reader.GetInt32(reader.GetOrdinal("AuthorizedBy_UserNumber"));
                                header.AuthorizerFullName = reader.IsDBNull(reader.GetOrdinal("AuthorizedBy_UserFullName"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("AuthorizedBy_UserFullName"));
                                header.AuthorizerEmail = reader.IsDBNull(reader.GetOrdinal("AuthorizedBy_UserEmail"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("AuthorizedBy_UserEmail"));

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

        public bool Insert(MovementRequestHeader header)
        {
            try
            {
                string query = @"
                    INSERT INTO MovementRequestHeaders (
                        iMovementRequestHeader,
                        iRequestedBy,
                        iAuthorizedBy,
                        MovementRequestHeaderDescription,
                        MovementRequestHeaderStatus,
                        MovementRequestHeaderCreatedAt,
                        MovementRequestHeaderAuthorizedAt
                    )
                    VALUES (
                        @iMovementRequestHeader,
                        @iRequestedBy,
                        @iAuthorizedBy,
                        @MovementRequestHeaderDescription,
                        @MovementRequestHeaderStatus,
                        @MovementRequestHeaderCreatedAt,
                        @MovementRequestHeaderAuthorizedAt
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@iMovementRequestHeader", header.iMovementRequestHeader);
                        insertCommand.Parameters.AddWithValue("@iRequestedBy", header.iRequestedBy);
                        insertCommand.Parameters.AddWithValue("@iAuthorizedBy", (object?)header.iAuthorizedBy ?? DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@MovementRequestHeaderDescription", (object?)header.MovementRequestHeaderDescription ?? DBNull.Value);
                        insertCommand.Parameters.AddWithValue("@MovementRequestHeaderStatus", header.MovementRequestHeaderStatus);
                        insertCommand.Parameters.AddWithValue("@MovementRequestHeaderCreatedAt", header.MovementRequestHeaderCreatedAt);
                        insertCommand.Parameters.AddWithValue("@MovementRequestHeaderAuthorizedAt", (object?)header.MovementRequestHeaderAuthorizedAt ?? DBNull.Value);
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

        public bool Update(MovementRequestHeader header)
        {
            try
            {
                string query = @"
                    UPDATE MovementRequestHeaders
                    SET
                        iRequestedBy = @iRequestedBy,
                        iAuthorizedBy = @iAuthorizedBy,
                        MovementRequestHeaderDescription = @MovementRequestHeaderDescription,
                        MovementRequestHeaderStatus = @MovementRequestHeaderStatus,
                        MovementRequestHeaderAuthorizedAt = @MovementRequestHeaderAuthorizedAt
                    WHERE iMovementRequestHeader = @iMovementRequestHeader;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@iMovementRequestHeader", header.iMovementRequestHeader);
                        updateCommand.Parameters.AddWithValue("@iRequestedBy", header.iRequestedBy);
                        updateCommand.Parameters.AddWithValue("@iAuthorizedBy", (object?)header.iAuthorizedBy ?? DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@MovementRequestHeaderDescription", (object?)header.MovementRequestHeaderDescription ?? DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@MovementRequestHeaderStatus", header.MovementRequestHeaderStatus);
                        updateCommand.Parameters.AddWithValue("@MovementRequestHeaderAuthorizedAt", (object?)header.MovementRequestHeaderAuthorizedAt ?? DBNull.Value);
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

        public bool Delete(Guid movementRequestHeaderId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM MovementRequestHeaders
                    WHERE iMovementRequestHeader = @iMovementRequestHeader;
                ";


                // obtenemos la conexion a la base de datos
                using (var connection = InventoryConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@iMovementRequestHeader", movementRequestHeaderId);
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
