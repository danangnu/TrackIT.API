using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class ticandskills
    {
        public int Id { get; set; }
        public int dbcandno { get; set; }
        public string dbkeyskilltype { get; set; }
        public string dbskillitem { get; set; }
        public string skillstatus { get; set; }
        public string staffid { get; set; }
        public DateTime created_at { get; set; }

        internal AppDb Db { get; set; }

        public ticandskills()
        {
        }

        internal ticandskills(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO ti_candskills SET dbcandno=@dbcandno, dbkeyskilltype= @dbkeyskilltype, dbskillitem= @dbskillitem, skillstatus = @skillstatus, staffid = @staffid";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE ti_candskills SET skillstatus = @skillstatus, staffid = @staffid, created_at = @created_at WHERE dbcandno=@dbcandno AND dbkeyskilltype= @dbkeyskilltype AND dbskillitem= @dbskillitem";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM ti_reportdocument WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
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
                ParameterName = "@dbcandno",
                DbType = DbType.Int32,
                Value = dbcandno,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbkeyskilltype",
                DbType = DbType.String,
                Value = dbkeyskilltype,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbskillitem",
                DbType = DbType.String,
                Value = dbskillitem,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@skillstatus",
                DbType = DbType.String,
                Value = skillstatus,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@staffid",
                DbType = DbType.String,
                Value = staffid,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@created_at",
                DbType = DbType.DateTime,
                Value = created_at,
            });
        }
    }
}