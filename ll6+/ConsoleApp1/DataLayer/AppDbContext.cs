using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RailwayTransport.Data
{
    public class AppDbContext : IDisposable
    {
        private readonly SqlConnection _connection;

        public AppDbContext()
        {
            _connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=RailwayTransport;Trusted_Connection=True;");
        }

        public SqlConnection GetConnection()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();

                _connection.Dispose();
            }
        }
    }
}