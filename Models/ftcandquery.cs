using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class ftcandquery
    {
        public AppDb Db { get; }

        public ftcandquery(AppDb db)
        {
            Db = db;
        }

        public async Task<ftcand> FindOneAsync(int Id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT id,dbcandno,fileName,fullPath,reportStatus,IFNULL(reportDesc, 'NULL') As reportDesc,created_at,status_email,updated_at,filesize,serverName,lastmodified FROM ti_reportdocument WHERE Id=@Id;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Value = Id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<ftcand>> FindListAsync(int Id)
        {    
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT id,dbcandno,fileName,fullPath,reportStatus,IFNULL(reportDesc, 'NULL') As reportDesc,created_at,status_email,updated_at,filesize,serverName,lastmodified FROM ti_reportdocument WHERE dbcandno = @Id;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Value = Id,
            });
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<ftcand>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT id,dbcandno,fileName,fullPath,reportStatus,IFNULL(reportDesc, 'NULL') As reportDesc,created_at,status_email,updated_at,filesize,serverName,lastmodified FROM ti_reportdocument ORDER BY Id DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM ti_reportdocument";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<ftcand>> ReadAllAsync(DbDataReader reader)
        {
            DateTime? sdate = new DateTime();
            sdate = Convert.ToDateTime("1971-01-01T00:00:00");
            var posts = new List<ftcand>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ftcand(Db)
                    {
                        Id = reader.GetInt32(0),
                        dbcandno = reader.GetInt32(1),
                        fileNames = reader.GetString(2),
                        fullPath = reader.GetString(3),
                        reportStatus = reader.GetString(4),
                        reportDesc = reader.GetString(5),
                        created_at = reader.GetDateTime(6),
                        status_email = reader.GetInt16(7),
                        updated_at = reader.GetDateTime(8),
                        filesize = (reader.IsDBNull(9) ? 0 : reader.GetDecimal(9)),
                        serverName = (reader.IsDBNull(10) ? null : reader.GetString(10)),
                        lastmodified = (DateTime)(reader.IsDBNull(11) ? sdate : reader.GetDateTime(11)),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}