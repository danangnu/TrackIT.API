using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class tiftcand
    {
        public int dbcandno { get; set; }
        public DateTime lastScanning { get; set; }
        internal AppDb Db { get; set; }

        public tiftcand()
        {
        }

        internal tiftcand(AppDb db)
        {
            Db = db;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE ti_ftcandidate SET lastScanning = @lastScanning WHERE dbcandno = @dbcandno;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbcandno",
                DbType = DbType.Int32,
                Value = dbcandno,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@lastScanning",
                DbType = DbType.DateTime,
                Value = lastScanning,
            });
        }
    }
}