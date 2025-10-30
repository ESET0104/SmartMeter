using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Data.Entities;

namespace SmartMeterWeb.Data.DataSeeder
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Tariff
            if (!context.Tariffs.Any())
            {
                var tariff = new Tariff
                {
                    Name = "Residential Standard",
                    EffectiveFrom = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-1)),
                    EffectiveTo = null,
                    BaseRate = 50, // fixed monthly charge
                    TaxRate = 5    // 5%
                };
                context.Tariffs.Add(tariff);
                context.SaveChanges();

                // TOD Rules
                context.TodRules.AddRange(new List<TodRule>
                {
                    new TodRule
                    {
                        TariffId = tariff.TariffId,
                        Name = "Peak Hours",
                        StartTime = new TimeOnly(18,0),
                        EndTime = new TimeOnly(22,0),
                        RatePerKwh = 0.20M,
                        IsDeleted = false
                    },
                    new TodRule
                    {
                        TariffId = tariff.TariffId,
                        Name = "Off-Peak",
                        StartTime = new TimeOnly(22,0),
                        EndTime = new TimeOnly(6,0),
                        RatePerKwh = 0.10M,
                        IsDeleted = false
                    }
                });

                // Slabs
                context.TariffSlabs.AddRange(new List<TariffSlab>
                {
                    new TariffSlab
                    {
                        TariffId = tariff.TariffId,
                        FromKwh = 0,
                        ToKwh = 100,
                        RatePerKwh = 0.12M,
                        IsDeleted = false
                    },
                    new TariffSlab
                    {
                        TariffId = tariff.TariffId,
                        FromKwh = 101,
                        ToKwh = 300,
                        RatePerKwh = 0.15M,
                        IsDeleted = false
                    },
                    new TariffSlab
                    {
                        TariffId = tariff.TariffId,
                        FromKwh = 301,
                        ToKwh = 10000,
                        RatePerKwh = 0.22M,
                        IsDeleted = false
                    }
                });

                context.SaveChanges();
            }

            // Consumer
            if (!context.Consumers.Any())
            {
                var consumer = new Consumer
                {
                    Name = "Ganesh A",
                    Phone = "1234567890",
                    Email = "ganesh@example.com",
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "system"
                };
                context.Consumers.Add(consumer);
                context.SaveChanges();

                // Meter
                var meter = new Meter
                {
                    MeterSerialNo = "MTR123456",
                    ConsumerId = consumer.ConsumerId,
                    IpAddress = "192.168.1.100",
                    ICCID = "ICCID123",
                    IMSI = "IMSI123",
                    Manufacturer = "SmartMeters Inc",
                    Firmware = "v1.0",
                    Category = "Residential",
                    InstallTsUtc = DateTimeOffset.UtcNow,
                    Status = "Active"
                };
                context.Meters.Add(meter);
                context.SaveChanges();

                // Sample hourly meter readings for 1 month
                var readings = new List<MeterReading>();
                var startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);

                for (int d = 0; d < 30; d++) // 30 days
                {
                    for (int h = 0; h < 24; h++) // 24 hours
                    {
                        readings.Add(new MeterReading
                        {
                            MeterId = meter.MeterSerialNo,
                            ReadingDate = startDate.AddDays(d).AddHours(h),
                            Voltage = 230,
                            Current = 5,
                            PowerFactor = 0.95,
                            EnergyConsumed = Math.Round((230 * 5 * 0.95 / 1000), 6), // kWh per hour
                        });
                    }
                }

                context.MeterReadings.AddRange(readings);
                context.SaveChanges();
            }
        }
    }
}
