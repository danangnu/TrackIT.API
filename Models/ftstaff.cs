using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class ftstaff
    {
        public int Id { get; set; }
        public int dbccommcandno { get; set; }
        public DateTime dbccommdate { get; set; }
        public DateTime dbccommupdate { get; set; }
        public string dbccommtime { get; set; }
        public string dbccomments { get; set; }
        public string dbccommdesc { get; set; }
        public string dbccommstaff { get; set; }

        internal AppDb Db { get; set; }

        public ftstaff()
        {
        }

        internal ftstaff(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO ftcandcomms( dbccommcandno, dbccommdate, dbccommupdate, dbccommtime, dbccomments, dbccommdesc, dbccommstaff) VALUES( @dbccommcandno, @dbccommdate, @dbccommupdate, @dbccommtime, @dbccomments, @dbccommdesc, @dbccommstaff)";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int) cmd.LastInsertedId;
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbccommcandno",
                DbType = DbType.Int32,
                Value = dbccommcandno,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbccommdate",
                DbType = DbType.DateTime,
                Value = dbccommdate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbccommupdate",
                DbType = DbType.DateTime,
                Value = dbccommupdate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbccommtime",
                DbType = DbType.String,
                Value = dbccommtime,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbccomments",
                DbType = DbType.String,
                Value = dbccomments,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbccommdesc",
                DbType = DbType.DateTime,
                Value = dbccommdesc,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbccommstaff",
                DbType = DbType.DateTime,
                Value = dbccommstaff,
            });
        }
    }
}