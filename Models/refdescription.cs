using System;

namespace TrackIT.API.Models
{
    public class refdescription
    {
        public string description { get; set; }

        internal AppDb Db { get; set; }

        public refdescription()
        {
        }

        internal refdescription(AppDb db)
        {
            Db = db;
        }
    }
}