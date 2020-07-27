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

        public async Task<List<ftcandidate>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbcandno FROM ftcandidate ORDER BY dbcandno DESC LIMIT 100;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<ftcandidate>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<ftcandidate>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ftcandidate(Db)
                    {
                        dbcandno = reader.GetInt32(0),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}