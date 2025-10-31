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
        public DbSet<Arrear> Arrears { get; set; }
        public DbSet<TariffDetails> TariffDetails { get; set; }
        public DbSet<OrgUnit> OrgUnits { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<CustomerCareMessage> CustomerCareMessages { get; set; }

        

        //To update database after making any changes in the classes, run these

        // Add-Migration <Update migration name>
        // Update-Database

        //in package manager console


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasSequence<int>("MeterSeq")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.Entity<Meter>(entity =>
            {
                entity.Property(m => m.MeterSerialNo)
                    .HasDefaultValueSql("'SM' || LPAD(nextval('\"MeterSeq\"')::text, 5, '0')");
            });

            modelBuilder.Entity<OrgUnit>()
                .ToTable(t => t.HasCheckConstraint("CK_Type", "\"Type\" IN ('Zone','Substation','Feeder','DTR')"));

            modelBuilder.Entity<OrgUnit>()
                .HasIndex(l => l.Type);

            modelBuilder.Entity<Tariff>()
                .ToTable(t => t.HasCheckConstraint("CK_Tariff_Dates", "\"EffectiveTo\" IS NULL OR \"EffectiveFrom\" < \"EffectiveTo\""));

            modelBuilder.Entity<Tariff>()
                .ToTable(t => t.HasCheckConstraint("CK_Tariff_BaseRate", "\"BaseRate\" > 0"));

            modelBuilder.Entity<TodRule>()
                .ToTable(t => t.HasCheckConstraint("CK_TodRule_Time", "\"EndTime\" > \"StartTime\""));

            modelBuilder.Entity<TodRule>()
                .ToTable(t => t.HasCheckConstraint("CK_TodRule_Rate", "\"RatePerKwh\" > 0"));

            modelBuilder.Entity<TodRule>()
                .HasIndex(l => l.Name);

            modelBuilder.Entity<TariffSlab>()
                .ToTable(t => t.HasCheckConstraint("CK_TariffSlab_Range", "\"FromKwh\" >= 0 AND \"ToKwh\" > \"FromKwh\""));

            modelBuilder.Entity<TariffSlab>()
                .ToTable(t => t.HasCheckConstraint("CK_TariffSlab_Rate", "\"RatePerKwh\" > 0"));

            modelBuilder.Entity<Consumer>()
                .ToTable(t => t.HasCheckConstraint("CK_Consumer_timestamp", "\"UpdatedAt\" IS NULL OR \"UpdatedAt\" > \"CreatedAt\""));

            modelBuilder.Entity<Consumer>()
                .HasIndex(l => l.Name);

            modelBuilder.Entity<Meter>()
                .ToTable(t => t.HasCheckConstraint("CK_Meter_Status", "\"Status\" IN ('Active','Inactive','Decommissioned')"));

            modelBuilder.Entity<Arrear>()
                .ToTable(t => t.HasCheckConstraint("CK_Arrear_Type", "\"ArrearType\" IN ('Overdue','Penalty','Interest')"));

            modelBuilder.Entity<Arrear>()
                .ToTable(t => t.HasCheckConstraint("CK_Arrear_Amount", "\"Amount\" >= 0"));

            modelBuilder.Entity<Arrear>()
                .ToTable(t => t.HasCheckConstraint("CK_Arrear_PaidStatus", "\"PaidStatus\" IN ('Paid','Unpaid','Partially Paid')"));

            modelBuilder.Entity<Billing>()
                .Property(i => i.TotalAmount)
                .HasComputedColumnSql("\"BaseAmount\" + \"TaxAmount\"", stored: true);

            modelBuilder.Entity<Billing>()
                .ToTable(t => t.HasCheckConstraint("CK_Billings_PaidStatus", "\"PaymentStatus\" IN ('Paid','Unpaid','Overdue','Cancelled')"));


        }
    }
}
