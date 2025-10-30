using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

//using SmartMeter.Data.Entities;
using SmartMeterWeb.Data.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using SmartMeterWeb.Data.Entities;

namespace SmartMeterWeb.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<TodRule> TodRules { get; set; }
        public DbSet<TariffSlab> TariffSlabs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Meter> Meters { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Arrears> Arrears { get; set; }
        public DbSet<TariffDetails> TariffDetails { get; set; }
        public DbSet<OrgUnit> OrgUnits { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<CustomerCareMessage> CustomerCareMessages { get; set; }

        

        //To update database after making any changes in the classes, run these

        // Add-Migration <Update migration name>
        // Update-Database

        //in package manager console


    }
}
