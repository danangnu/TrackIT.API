using System;

namespace TrackIT.API.Models
{
    public class ftkeywords
    {
        public string dbkeyskilltype { get; set; }
        public string Keyword { get; set; }

        internal AppDb Db { get; set; }

        public ftkeywords()
        {
        }

        internal ftkeywords(AppDb db)
        {
            Db = db;
        }
    }
}