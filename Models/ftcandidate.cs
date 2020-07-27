using System;

namespace TrackIT.API.Models
{
    public class ftcandidate
    {
        public int dbcandno { get; set; }
        public DateTime lastScanning { get; set; }

        internal AppDb Db { get; set; }

        public ftcandidate()
        {
        }

        internal ftcandidate(AppDb db)
        {
            Db = db;
        }
    }
}