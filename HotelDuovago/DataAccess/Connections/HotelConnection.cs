using System.Configuration;

using Microsoft.Data.SqlClient;

namespace DataAccess.Connections
{
    public static class HotelConnection
    {
        private static readonly string connectionString;

        static HotelConnection()
        {
            var settings = ConfigurationManager.ConnectionStrings["HotelConnection"];
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ConfigurationErrorsException("Connection string not found");
            }
            connectionString = settings.ConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
