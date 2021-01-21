using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace TrackIT.API.Models
{
    public class ftkeywordquery
    {
        public AppDb Db { get; }

        public ftkeywordquery(AppDb db)
        {
            Db = db;
        }

        public async Task<ftkeyword> FindOneAsync(int Id)
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

        public async Task<List<ftkeyword>> FindAvailAsync(string startdate, string enddate, int days)
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

        public async Task<List<ftkeyword>> FindAvail2Async(int Id, int Ids, string startdate, string enddate, int days)
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

        public async Task<List<ftkeyword>> MasterKeywordAsync()
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