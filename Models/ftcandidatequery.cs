using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class ftcandidatequery
    {
        public AppDb Db { get; }

        public ftcandidatequery(AppDb db)
        {
            Db = db;
        }

        public async Task<ftcandidate> FindOneAsync(int Id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbcandno FROM ftcandidate WHERE dbcandno=@Id;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Value = Id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<ftcandidate>> FindListAsync(int Id, int Ids, int days)
        {    
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT ftCandidate.dbCandno, ti_ftcandidate.lastScanning FROM ftCandidate LEFT JOIN `ti_ftcandidate` ON ftcandidate.dbcandno = `ti_ftcandidate`.`dbcandno` WHERE ftcandidate.dbcandno >= @Id AND ftcandidate.dbcandno <= @Ids AND (ti_ftcandidate.lastScanning IS NULL OR ti_ftcandidate.`lastScanning` < DATE_SUB(CURDATE(), INTERVAL @days DAY)) ORDER BY dbcandavaildate DESC, dbCandno DESC;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Value = Id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Ids",
                DbType = DbType.Int32,
                Value = Ids,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@days",
                DbType = DbType.Int32,
                Value = days,
            });
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<ftcandidate>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbcandno FROM ftcandidate ORDER BY dbcandno DESC LIMIT 100;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<ftcandidate>> ReadAllAsync(DbDataReader reader)
        {
            DateTime? sdate = new DateTime();
            sdate = Convert.ToDateTime("1971-01-01T00:00:00");
            var posts = new List<ftcandidate>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ftcandidate(Db)
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