namespace TrackIT.API.Models
{
    public class ftcandidate
    {
        public int dbcandno { get; set; }

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