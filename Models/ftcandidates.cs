using System;

namespace TrackIT.API.Models
{
    public class ftcandidates
    {
        public int dbcandno { get; set; }
        public string dbcandnames { get; set; }
        public string dbcandsurname { get; set; }
        public DateTime dbcanddob { get; set; }
        public string dbcandexpsalary { get; set; }

        internal AppDb Db { get; set; }

        public ftcandidates()
        {
        }

        internal ftcandidates(AppDb db)
        {
            Db = db;
        }
    }
}