using Microsoft.EntityFrameworkCore;
using SmartMeterWeb.Data.Entities;

namespace SmartMeterWeb.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Consumer> Consumers { get; set; }

        //public override void OnModelCreating
    }
}
