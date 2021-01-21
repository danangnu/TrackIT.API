using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class loginquery
    {
        public AppDb Db { get; }

        public loginquery(AppDb db)
        {
            Db = db;
        }

        public async Task<login> FindOneAsync(string username,string password)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbstffid, dbinuse FROM ftstaff WHERE dbstffid=@username AND dbstffpswd=@password";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@username",
                DbType = DbType.String,
                Value = username,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@password",
                DbType = DbType.String,
                Value = password,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }


        public async Task<List<login>> AllUserAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbstffid, dbinuse FROM ftstaff";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<login>> ReadAllAsync(DbDataReader reader)
        {           
            var posts = new List<login>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new login(Db)
                    {
                        dbstffid = reader.GetString(0),
                        dbinuse = reader.GetString(1),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}