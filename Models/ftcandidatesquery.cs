using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class ftcandidatesquery
    {
        public AppDb Db { get; }

        public ftcandidatesquery(AppDb db)
        {
            Db = db;
        }

        public async Task<ftcandidates> FindOneAsync(int Id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbcandno, dbcandnames, dbcandsurname, dbcanddob, dbcandexpsalary FROM ftcandidate WHERE dbcandno=@Id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Value = Id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<ftcandidates>> FindListAsync(int Id, int Ids, int days)
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

        public async Task<List<ftcandidates>> FindAvailAsync(string startdate, string enddate, int days)
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

        public async Task<List<ftcandidates>> FindAvail2Async(int Id, int Ids, string startdate, string enddate, int days)
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

        public async Task<List<ftcandidates>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT dbcandno FROM ftcandidate ORDER BY dbcandno DESC LIMIT 100;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<ftcandidates>> ReadAllAsync(DbDataReader reader)
        {
            DateTime? sdate = new DateTime();
            sdate = Convert.ToDateTime("1900-01-01T00:00:00");
            var posts = new List<ftcandidates>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ftcandidates(Db)
                    {
                        dbcandno = reader.GetInt32(0),
                        dbcandnames = reader.GetString(1),
                        dbcandsurname = reader.GetString(2),
                        dbcanddob = (DateTime)(reader.IsDBNull(3) ? sdate : reader.GetDateTime(3)),
                        dbcandexpsalary = reader.GetString(4),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}