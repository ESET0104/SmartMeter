using Microsoft.EntityFrameworkCore;
using SmartMeterWeb.Data.Entities;

namespace SmartMeterWeb.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Consumer> Consumers { get; set; }

        //To update database after making any changes in the classes, run these

        // Add-Migration <Update migration name>
        // Update-Database

        //in package manager console

    }
}
