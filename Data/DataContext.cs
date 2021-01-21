using Microsoft.EntityFrameworkCore;

namespace TrackIT.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppDb> Users { get; set; }
    }
}