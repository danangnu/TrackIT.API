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
        public int grade { get; set; }
        public string reason { get; set; }
        public string licence_number { get; set; }
        public string issued_date { get; set; }
        public string expiry_date { get; set; }
        public int comment_id { get; set; }
        public int dbskillgrp { get; set; }

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
            cmd.CommandText = @"INSERT INTO ti_reportdocument (dbcandno, fileName, fullPath, reportStatus, reportDesc) SELECT * FROM (SELECT @dbcandno AS dbcandno, @fileNames AS fileName, @fullPath AS fullPath, @reportStatus AS reportStatus, @reportDesc AS reportDesc) AS tmp WHERE NOT EXISTS (SELECT fullPath FROM ti_reportdocument WHERE id = @Id) LIMIT 1;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE ti_reportdocument SET reportStatus = @reportStatus, status_email = @status_email, reportDesc = @reportDesc WHERE Id = @Id;";
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
                ParameterName = "@grade",
                DbType = DbType.Int32,
                Value = grade,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@staffid",
                DbType = DbType.String,
                Value = staffid,
            });
        }
    }
}