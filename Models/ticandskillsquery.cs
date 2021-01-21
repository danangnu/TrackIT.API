using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class ticandskillsquery
    {
        public AppDb Db { get; }

        public ticandskillsquery(AppDb db)
        {
            Db = db;
        }

        public async Task<ticandskills> FindOneAsync(int Id, string skilltype, string skillitem)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbcandno,dbkeyskilltype,dbskillitem,skillstatus,staffid,created_at FROM ti_candskills WHERE dbcandno=@Id AND dbkeyskilltype= @skilltype AND dbskillitem= @skillitem";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Value = Id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@skilltype",
                DbType = DbType.String,
                Value = skilltype,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@skillitem",
                DbType = DbType.String,
                Value = skillitem,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<ticandskills>> FindListAsync(int Id, int Ids, int days)
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

        public async Task<List<ticandskills>> FindAvailAsync(string startdate, string enddate, int days)
        {    
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT ftCandidate.dbCandno,ti_ftcandidate.lastScanning FROM ftCandidate LEFT JOIN `ti_ftcandidate` ON ftcandidate.dbcandno = `ti_ftcandidate`.`dbcandno` WHERE ftCandidate.dbcandavaildate >= @startdate AND ftCandidate.dbcandavaildate <= @enddate AND (ti_ftcandidate.lastScanning IS NULL OR (ti_ftcandidate.`lastScanning` < DATE_SUB(CURDATE(), INTERVAL @days DAY))) ORDER BY dbcandavaildate DESC, dbCandno DESC;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@startdate",
                DbType = DbType.String,
                Value = startdate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@enddate",
                DbType = DbType.String,
                Value = enddate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@days",
                DbType = DbType.Int32,
                Value = days,
            });
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<ticandskills>> FindAvail2Async(int Id, int Ids, string startdate, string enddate, int days)
        {    
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT ftCandidate.dbCandno,ti_ftcandidate.lastScanning FROM ftCandidate LEFT JOIN `ti_ftcandidate` ON ftcandidate.dbcandno = `ti_ftcandidate`.`dbcandno` WHERE ftcandidate.dbcandno >= @Id AND ftcandidate.dbcandno <= @Ids AND (ftCandidate.dbcandavaildate >= @startdate AND ftCandidate.dbcandavaildate <= @enddate) AND (ti_ftcandidate.lastScanning IS NULL OR (ti_ftcandidate.`lastScanning` < DATE_SUB(CURDATE(), INTERVAL @days DAY))) ORDER BY dbcandavaildate DESC, dbCandno DESC;";
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
                ParameterName = "@startdate",
                DbType = DbType.String,
                Value = startdate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@enddate",
                DbType = DbType.String,
                Value = enddate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@days",
                DbType = DbType.Int32,
                Value = days,
            });
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<ticandskills>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbcandno FROM ftcandidate ORDER BY dbcandno DESC LIMIT 100;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<ticandskills>> ReadAllAsync(DbDataReader reader)
        {
            DateTime? sdate = new DateTime();
            sdate = Convert.ToDateTime("1971-01-01T00:00:00");
            var posts = new List<ticandskills>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ticandskills(Db)
                    {
                        dbcandno = reader.GetInt32(0),
                        dbkeyskilltype = reader.GetString(1),
                        dbskillitem = reader.GetString(2),
                        skillstatus = reader.GetString(3),
                        staffid = reader.GetString(4),
                        created_at = (DateTime)(reader.IsDBNull(5) ? sdate : reader.GetDateTime(5)),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}