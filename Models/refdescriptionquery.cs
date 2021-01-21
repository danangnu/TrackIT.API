using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class refdescriptionquery
    {
        public AppDb Db { get; }

        public refdescriptionquery(AppDb db)
        {
            Db = db;
        }


        public async Task<List<refdescription>> DescriptAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT description FROM refdescription WHERE desctype='Candidate' ORDER BY description ASC";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<refdescription>> ReadAllAsync(DbDataReader reader)
        {           
            var posts = new List<refdescription>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new refdescription(Db)
                    {
                        description = reader.GetString(0),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}