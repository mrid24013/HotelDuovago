using DataAccess.Connections;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Numerics;

namespace DataAccess.Repositories
{
    public class HabitacionRepository
    {
        public List<Habitacion> GetAll()
        {
            List<Habitacion> habitaciones = new List<Habitacion>() { };
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        HabitacionId,
                        Numero,
                        Tipo,
                        Precio,
                        Capacidad,
                        Descripcion,
                        Disponible
                    FROM Habitaciones
                    ORDER BY Numero ASC;
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
                                habitaciones.Add(new Habitacion()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("HabitacionId")),
                                    Numero = reader.GetInt32(reader.GetOrdinal("Numero")),
                                    Tipo = reader.GetString(reader.GetOrdinal("Tipo")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                    Capacidad = reader.GetInt32(reader.GetOrdinal("Capacidad")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Disponible = reader.GetBoolean(reader.GetOrdinal("Disponible"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                habitaciones = new List<Habitacion>() { };
            }

            return habitaciones;
        }

        //=====================================================================================================================

        public bool Find(Habitacion habitacion)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        HabitacionId,
                        Numero,
                        Tipo,
                        Precio,
                        Capacidad,
                        Descripcion,
                        Disponible
                    FROM Habitaciones
                    WHERE HabitacionId = @HabitacionId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HabitacionId", habitacion.Id);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                habitacion.Numero = reader.GetInt32(reader.GetOrdinal("Nombre"));
                                habitacion.Tipo = reader.GetString(reader.GetOrdinal("Telefono"));
                                habitacion.Precio = reader.GetDecimal(reader.GetOrdinal("Email"));
                                habitacion.Capacidad = reader.GetInt32(reader.GetOrdinal("Direccion"));
                                habitacion.Descripcion = reader.GetString(reader.GetOrdinal("FechaRegistro"));
                                habitacion.Disponible = reader.GetBoolean(reader.GetOrdinal("FechaRegistro"));
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

        public bool FindID(int HabitacionId)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        HabitacionId
                    FROM Habitaciones
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

        public bool FindNumero(int Numero)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT 
                        Numero
                    FROM Habitaciones
                    WHERE Numero = @Numero;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Numero", Numero);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Numero = reader.GetInt32(reader.GetOrdinal("Numero"));
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

        public bool FindTipo(string Tipo)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        Tipo
                    FROM Habitaciones
                    WHERE Tipo = @Tipo;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Tipo", Tipo.ToLower());
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Tipo = reader.GetString(reader.GetOrdinal("Tipo"));
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

        public bool FindPrecio(decimal Precio)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        Precio
                    FROM Habitaciones
                    WHERE Precio = @Precio;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Precio", Precio);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio"));
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

        public bool FindCapacidad(int Capacidad)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        Capacidad
                    FROM Habitaciones
                    WHERE Capacidad = @Capacidad;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Capacidad", Capacidad);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Capacidad = reader.GetInt32(reader.GetOrdinal("Capacidad"));
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

        public bool FindDescripcion(String Descripcion)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        Descripcion
                    FROM Habitaciones
                    WHERE Descripcion = @Descripcion;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Descripcion", Descripcion.ToLower());
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
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

        public bool FindDisponible(Boolean Disponible)
        {
            try
            {
                // generamos la query
                string query = @"
                    SELECT
                        Disponible
                    FROM Habitaciones
                    WHERE Disponible = @Disponible;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Disponible", Disponible);
                        // abrimos la conexion 
                        connection.Open();
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            // verifica si el reader obtuvo alguna fila
                            if (reader.Read())
                            {
                                // poblamos con el resultado
                                Disponible = reader.GetBoolean(reader.GetOrdinal("Disponible"));
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

        //=====================================================================================================================

        public bool Insert(Habitacion habitacion)
        {
            try
            {
                // declaramos y generamos el query de insercion
                string query = @"
                    INSERT INTO Habitaciones (
                        Numero,
                        Tipo,
                        Precio,
                        Capacidad,
                        Descripcion,
                        Disponible
                    )
                    VALUES (
                        @Numero,
                        @Tipo,
                        @Precio,
                        @Capacidad,
                        @Descripcion,
                        @Disponible
                    );
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var insertCommand = new SqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Numero", habitacion.Numero);
                        insertCommand.Parameters.AddWithValue("@Tipo", habitacion.Tipo);
                        insertCommand.Parameters.AddWithValue("@Precio", habitacion.Precio);
                        insertCommand.Parameters.AddWithValue("@Capacidad", habitacion.Capacidad);
                        insertCommand.Parameters.AddWithValue("@Descripcion", habitacion.Descripcion);
                        insertCommand.Parameters.AddWithValue("@Disponible", habitacion.Disponible);
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

        public bool Update(Habitacion habitacion)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    UPDATE Habitaciones
                    SET
                        Numero = @Numero,
                        Tipo = @Tipo,
                        Precio = @Precio,
                        Capacidad = @Capacidad,
                        Descripcion = @Descripcion,
                        Disponible = @Disponible
                    WHERE HabitacionId = @HabitacionId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var updateCommand = new SqlCommand(query, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@HabitacionId", habitacion.Id);
                        updateCommand.Parameters.AddWithValue("@Numero", habitacion.Numero);
                        updateCommand.Parameters.AddWithValue("@Tipo", habitacion.Tipo);
                        updateCommand.Parameters.AddWithValue("@Precio", habitacion.Precio);
                        updateCommand.Parameters.AddWithValue("@Capacidad", habitacion.Capacidad);
                        updateCommand.Parameters.AddWithValue("@Descripcion", habitacion.Descripcion);
                        updateCommand.Parameters.AddWithValue("@Disponible", habitacion.Disponible);
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

        public bool Delete(int HabitacionId)
        {
            try
            {
                // declaramos y generamos el query de busqueda
                string query = @"
                    DELETE FROM Habitaciones
                    WHERE HabitacionId = @HabitacionId;
                ";

                // obtenemos la conexion a la base de datos
                using (var connection = HotelConnection.GetConnection())
                {
                    // genera el comando indicando la query a ejecutar y la conexion en la que se ejecutara
                    using (var deleteCommand = new SqlCommand(query, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@HabitacionId", HabitacionId);
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
