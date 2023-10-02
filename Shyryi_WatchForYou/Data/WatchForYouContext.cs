using Microsoft.EntityFrameworkCore;
using Shyryi_WatchForYou.DTOs;

namespace Shyryi_WatchForYou.Data
{
    public class WatchForYouContext : DbContext
    {
        public DbSet<UserDto> User { get; set; }
        public DbSet<AreaDto> Area { get; set; }
        public DbSet<ThingDto> Thing { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WatchForYou;Trusted_Connection=True;");
        }
    }
}
