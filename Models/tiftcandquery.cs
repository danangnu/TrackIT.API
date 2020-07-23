using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class tiftcandquery
    {
        public AppDb Db { get; }

        public tiftcandquery(AppDb db)
        {
            Db = db;
        }

        public async Task<tiftcand> FindOneAsync(int dbcandno)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbcandno,lastScanning FROM ti_ftcandidate WHERE dbcandno=@dbcandno;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbcandno",
                DbType = DbType.Int32,
                Value = dbcandno,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<tiftcand>> FindListAsync(int dbcandno)
        {    
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbcandno,lastScanning FROM ti_ftcandidate WHERE dbcandno=@dbcandno;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@dbcandno",
                DbType = DbType.Int32,
                Value = dbcandno,
            });
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<tiftcand>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbcandno,lastScanning FROM ti_ftcandidate ORDER BY dbcandno DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<tiftcand>> ReadAllAsync(DbDataReader reader)
        {
            DateTime? sdate = new DateTime();
            sdate = Convert.ToDateTime("1971-01-01T00:00:00");
            var posts = new List<tiftcand>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new tiftcand(Db)
                    {
                        dbcandno = reader.GetInt32(0),
                        lastScanning = (DateTime)(reader.IsDBNull(1) ? sdate : reader.GetDateTime(1)),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}