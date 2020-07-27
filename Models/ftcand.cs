using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class ftcand
    {
        public int Id { get; set; }
        public int dbcandno { get; set; }
        public string fileNames { get; set; }
        public string fullPath { get; set; }
        public string reportStatus { get; set; }
        public string reportDesc { get; set; }
        public DateTime created_at { get; set; }
        public int status_email { get; set; }
        public DateTime updated_at { get; set; }
        public decimal filesize { get; set; }
        public string serverName { get; set; }
        public DateTime lastmodified { get; set; }

        internal AppDb2 Db { get; set; }

        public ftcand()
        {
        }

        internal ftcand(AppDb2 db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            //cmd.CommandText = @"INSERT INTO ti_reportdocument (dbcandno, fileName, fullPath, reportStatus, reportDesc, status_email, updated_at, created_at, serverName, filesize, lastmodified) SELECT * FROM (SELECT @dbcandno AS dbcandno, @fileName AS fileName, @fullPath AS fullPath, @reportStatus AS reportStatus, @reportDesc AS reportDesc, @status_email AS status_email, @updated_at AS updated_at, @created_at AS created_at, @serverName AS serverName, @filesize AS filesize, @lastmodified AS lastmodified) AS tmp WHERE NOT EXISTS (SELECT fullPath FROM ti_reportdocument WHERE fullPath = @fullPath AND dbcandno = @dbcandno) LIMIT 1;";
            cmd.CommandText = @"INSERT INTO ti_reportdocument (dbcandno, fileName, fullPath, reportStatus, reportDesc, status_email, updated_at, serverName, filesize, lastmodified) VALUES (@dbcandno, @fileNames, @fullPath, @reportStatus, @reportDesc,@status_email,@updated_at,@serverName, @filesize, @lastmodified);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE ti_reportdocument SET reportStatus = @reportStatus, status_email = @status_email, reportDesc = @reportDesc, updated_at = @updated_at, serverName = @serverName, filesize = @filesize, lastmodified = @lastmodified WHERE Id = @Id;";
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
                ParameterName = "@fileNames",
                DbType = DbType.String,
                Value = fileNames,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@fullPath",
                DbType = DbType.String,
                Value = fullPath,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@reportStatus",
                DbType = DbType.String,
                Value = reportStatus,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@reportDesc",
                DbType = DbType.String,
                Value = reportDesc,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@created_at",
                DbType = DbType.DateTime,
                Value = created_at,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@status_email",
                DbType = DbType.Int16,
                Value = status_email,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@updated_at",
                DbType = DbType.DateTime,
                Value = updated_at,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@filesize",
                DbType = DbType.Decimal,
                Value = filesize,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@serverName",
                DbType = DbType.String,
                Value = serverName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@lastmodified",
                DbType = DbType.DateTime,
                Value = lastmodified,
            });
        }
    }
}