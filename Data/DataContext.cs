using Microsoft.EntityFrameworkCore;
using TrackIT.API.Entities;

namespace TrackIT.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AppStaff> ftstaff { get; set; }
    }
}