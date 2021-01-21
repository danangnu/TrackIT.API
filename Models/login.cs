using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class login
    {
        public string dbstffid { get; set; }
        public string dbinuse { get; set; }
        public string dbstffpswd { get; set; }

        internal AppDb Db { get; set; }

        public login()
        {
        }

        internal login(AppDb db)
        {
            Db = db;
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbstffid",
                DbType = DbType.String,
                Value = dbstffid,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbinuse",
                DbType = DbType.String,
                Value = dbinuse,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbstffpswd",
                DbType = DbType.String,
                Value = dbstffpswd,
            });
        }
    }
}