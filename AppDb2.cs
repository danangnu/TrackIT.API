using System;
using MySqlConnector;

namespace TrackIT.API
{
    public class AppDb2 : IDisposable
    {
        public MySqlConnection Connection { get; }

        public AppDb2(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}