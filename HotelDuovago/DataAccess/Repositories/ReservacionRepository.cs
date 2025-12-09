using DataAccess.Connections;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.Repositories
{
    public class ReservacionRepository
    {
        public List<Reservacion> GetAll()
        {
            List<Reservacion> reservaciones = new List<Reservacion>() { };
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        ReservacionId,
                        ClienteId,
                        HabitacionId,
                        FechaEntrada,
                        FechaSalida,
                        DiasEstancia,
                        MontoTotal,
                        Estado
                    FROM Reservaciones
                    ORDER BY ReservacionId ASC;
                ";
                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
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
                                reservaciones.Add(new Reservacion()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ReservacionId")),
                                    ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                                    HabitacionId = reader.GetInt32(reader.GetOrdinal("HabitacionId")),
                                    FechaEntrada = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaEntrada"))),
                                    FechaSalida = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaSalida"))),
                                    DiasEstancia = reader.GetInt32(reader.GetOrdinal("DiasEstancia")),
                                    MontoTotal = reader.GetDecimal(reader.GetOrdinal("MontoTotal")),
                                    Estado = reader.GetString(reader.GetOrdinal("Estado"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                reservaciones = new List<Reservacion>() { };
            }

            return reservaciones;
        }

        //=====================================================================================================================

        public bool Find(Reservacion reservacion)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        ClienteId,
                        HabitacionId,
                        FechaEntrada,
                        FechaSalida,
                        DiasEstancia,
                        MontoTotal,
                        Estado
                    FROM Reservaciones
                    WHERE ReservacionId = @ReservacionId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReservacionId", reservacion.Id);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                reservacion.ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                                reservacion.HabitacionId = reader.GetInt32(reader.GetOrdinal("HabitacionId"));
                                reservacion.FechaEntrada = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaEntrada")));
                                reservacion.FechaSalida = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaSalida")));
                                reservacion.DiasEstancia = reader.GetInt32(reader.GetOrdinal("DiasEstancia"));
                                reservacion.MontoTotal = reader.GetDecimal(reader.GetOrdinal("MontoTotal"));
                                reservacion.Estado = reader.GetString(reader.GetOrdinal("Estado"));
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

        public bool FindID(int ReservacionId)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        ReservacionId
                    FROM Reservaciones
                    WHERE ReservacionId = @ReservacionId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReservacionId", ReservacionId);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                ReservacionId = reader.GetInt32(reader.GetOrdinal("ReservacionId"));
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

        public bool FindClienteID(int ClienteId)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        ClienteId
                    FROM Reservaciones
                    WHERE ClienteId = @ClienteId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteId", ClienteId);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
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

        public bool FindHabitacionID(int HabitacionId)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        HabitacionId
                    FROM Reservaciones
                    WHERE HabitacionId = @HabitacionId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HabitacionId", HabitacionId);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                HabitacionId = reader.GetInt32(reader.GetOrdinal("HabitacionId"));
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

        public bool FindFechaEntrada(DateOnly FechaEntrada)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        FechaEntrada
                    FROM Reservaciones
                    WHERE FechaEntrada = @FechaEntrada;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FechaEntrada", FechaEntrada);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                FechaEntrada = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaEntrada")));
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

        public bool FindFechaSalida(DateOnly FechaSalida)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        FechaSalida
                    FROM Reservaciones
                    WHERE FechaSalida = @FechaSalida;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FechaSalida", FechaSalida);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                FechaSalida = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaSalida")));
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

        public bool FindDiasEstancia(int DiasEstancia)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        DiasEstancia
                    FROM Reservaciones
                    WHERE DiasEstancia = @DiasEstancia;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DiasEstancia", DiasEstancia);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                DiasEstancia = reader.GetInt32(reader.GetOrdinal("DiasEstancia"));
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

        public bool FindMontoTotal(decimal MontoTotal)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        MontoTotal
                    FROM Reservaciones
                    WHERE MontoTotal = @MontoTotal;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MontoTotal", MontoTotal);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                MontoTotal = reader.GetDecimal(reader.GetOrdinal("MontoTotal"));
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

        public bool FindEstado(string Estado)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        Estado
                    FROM Reservaciones
                    WHERE Estado = @Estado;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Estado", Estado.ToLower());
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Estado = reader.GetString(reader.GetOrdinal("Estado"));
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

        public bool FindReservacionExistente(int id, DateOnly entrada, DateOnly salida)
        {
            try
            {
                String[] entradaFormat = entrada.ToString().Split("/");
                String[] salidaFormat = salida.ToString().Split("/");
                DateOnly fEntrada = DateOnly.Parse(entradaFormat[2] + "/" + entradaFormat[0] + "/" + entradaFormat[1]);
                DateOnly fSalida = DateOnly.Parse(salidaFormat[2] + "/" + salidaFormat[0] + "/" + salidaFormat[1]);
                // generamos la query
                string query = @"
                    SELECT
                        ReservacionId
                    FROM Reservaciones
                    WHERE ClienteId = @ClienteId AND (FechaEntrada = @FechaEntrada OR FechaSalida = @FechaSalida)
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClienteId", id);
                        command.Parameters.AddWithValue("@FechaEntrada", entrada);
                        command.Parameters.AddWithValue("@FechaSalida", salida);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                id = reader.GetInt32(reader.GetOrdinal("ReservacionId"));
                                entrada = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("ReservacionId")));
                                salida = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("ReservacionId")));
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

        public (int, int) FindValues(int id)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        ClienteId,
                        HabitacionId
                    FROM Reservaciones
                    WHERE ReservacionId = @ReservacionId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReservacionId", id);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                return (reader.GetInt32(reader.GetOrdinal("ClienteId")),
                                    reader.GetInt32(reader.GetOrdinal("HabitacionId")));
                            }

                            // si el reader no obtuvo nada, no se encontró
                            return (0, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (0, 0);
            }
        }

        //=====================================================================================================================

        public bool Insert(Reservacion reservacion)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO Reservaciones (
                        ClienteId,
                        HabitacionId,
                        FechaEntrada,
                        FechaSalida,
                        DiasEstancia,
                        MontoTotal,
                        Estado
                    )
                    VALUES (
                        @ClienteId,
                        @HabitacionId,
                        @FechaEntrada,
                        @FechaSalida,
                        @DiasEstancia,
                        @MontoTotal,
                        @Estado
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@ClienteId", reservacion.ClienteId);
                        insertCommand.Parameters.AddWithValue("@HabitacionId", reservacion.HabitacionId);
                        insertCommand.Parameters.AddWithValue("@FechaEntrada", reservacion.FechaEntrada);
                        insertCommand.Parameters.AddWithValue("@FechaSalida", reservacion.FechaSalida);
                        insertCommand.Parameters.AddWithValue("@DiasEstancia", reservacion.DiasEstancia);
                        insertCommand.Parameters.AddWithValue("@MontoTotal", reservacion.MontoTotal);
                        insertCommand.Parameters.AddWithValue("@Estado", reservacion.Estado);
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

        public bool Update(Reservacion reservacion)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    UPDATE Reservaciones
                    SET
                        ClienteId = @ClienteId,
                        HabitacionId = @HabitacionId,
                        FechaEntrada = @FechaEntrada,
                        FechaSalida = @FechaSalida,
                        DiasEstancia = @DiasEstancia,
                        MontoTotal = @MontoTotal,
                        Estado = @Estado
                    WHERE ReservacionId = @ReservacionId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@ReservacionId", reservacion.Id);
                        updateCommand.Parameters.AddWithValue("@ClienteId", reservacion.ClienteId);
                        updateCommand.Parameters.AddWithValue("@HabitacionId", reservacion.HabitacionId);
                        updateCommand.Parameters.AddWithValue("@FechaEntrada", reservacion.FechaEntrada);
                        updateCommand.Parameters.AddWithValue("@FechaSalida", reservacion.FechaSalida);
                        updateCommand.Parameters.AddWithValue("@DiasEstancia", reservacion.DiasEstancia);
                        updateCommand.Parameters.AddWithValue("@MontoTotal", reservacion.MontoTotal);
                        updateCommand.Parameters.AddWithValue("@Estado", reservacion.Estado);
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

        public bool Delete(int ReservacionId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM Reservaciones
                    WHERE ReservacionId = @ReservacionId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@ReservacionId", ReservacionId);
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
