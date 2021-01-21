using System;

namespace TrackIT.API.Models
{
    public class ftkeyword
    {
        public string dbkeyskilltype { get; set; }
        public string Keyword { get; set; }

        internal AppDb Db { get; set; }

        public ftkeyword()
        {
        }

        internal ftkeyword(AppDb db)
        {
            Db = db;
        }
    }
}