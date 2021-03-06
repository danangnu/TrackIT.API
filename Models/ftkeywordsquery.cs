using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class ftkeywordsquery
    {
        public AppDb Db { get; }

        public ftkeywordsquery(AppDb db)
        {
            Db = db;
        }

        public async Task<List<ftkeyword>> FindListAsync(int Id, int Ids, int days)
        {    
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbkeyskilltype, dbkeyword AS 'Keyword' FROM ftkeywords INNER JOIN ti_keyword ON ftkeywords.`KeywordId` = ti_keyword.`idkeyword` WHERE quickkeyword = 1";
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

        public async Task<List<ftkeyword>> MasterKeywordsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbkeyskilltype, dbkeyword AS 'Keyword' FROM ftkeywords INNER JOIN ti_keyword ON ftkeywords.`KeywordId` = ti_keyword.`idkeyword` WHERE quickkeyword = 1";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<ftkeyword>> ReadAllAsync(DbDataReader reader)
        {           
            var posts = new List<ftkeyword>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ftkeyword(Db)
                    {
                        dbkeyskilltype = reader.GetString(0),
                        Keyword = reader.GetString(1),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}