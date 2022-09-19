using Course2.Models;
using Microsoft.EntityFrameworkCore;

namespace Course2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee>? Employees { get; set; }

        public DbSet<Video>? Videos { get; set; }

        public DbSet<Position>? Positions { get; set; }

        public DbSet<LogWatchVideo> LogWatchVideos { get; set; }

    }
}
