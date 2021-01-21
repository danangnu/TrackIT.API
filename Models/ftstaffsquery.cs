using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class ftstaffsquery
    {
        public AppDb Db { get; }

        public ftstaffsquery(AppDb db)
        {
            Db = db;
        }

        public async Task<ftstaffs> FindOneAsync(string username,string password)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbstffid, dbinuse, dbstffpswd FROM ftstaff WHERE dbstffid=@username AND dbstffpswd=@password";
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


        public async Task<List<ftstaffs>> AllUserAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbstffid, dbinuse FROM ftstaff";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<ftstaffs>> ReadAllAsync(DbDataReader reader)
        {           
            var posts = new List<ftstaffs>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ftstaffs(Db)
                    {
                        dbstffid = reader.GetString(0),
                        dbinuse = reader.GetString(1),
                        dbstffpswd = reader.GetString(2),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}