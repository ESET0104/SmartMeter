using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Models.Reports;
using Microsoft.EntityFrameworkCore;

namespace SmartMeterWeb.Services
{
    public class ReportService
    {

        private readonly AppDbContext _context;
        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MonthlyTariffReportDto>> GetMonthlyTariffReportAsync(DateTime fromDate, DateTime toDate)

        {
            var from_date = DateOnly.FromDateTime(fromDate);
            var to_date = DateOnly.FromDateTime(toDate);

            var query = from b in _context.Billings
                        join c in _context.Consumers on b.ConsumerId equals c.ConsumerId
                        join t in _context.Tariffs on c.TariffId equals t.TariffId
                        where b.BillingPeriodStart >= from_date && b.BillingPeriodEnd <= to_date
                        group new { b, c, t } by t.Name into g
                        select new MonthlyTariffReportDto
                        {
                            TariffName = g.Key,
                            TotalConsumers = g.Select(x => x.c.ConsumerId).Distinct().Count(),
                            TotalUnits = g.Sum(x => x.b.TotalUnitsConsumed),
                            BaseRevenue = g.Sum(x => x.b.BaseAmount),
                            TaxCollected = g.Sum(x => x.b.TaxAmount),
                            TotalRevenue = g.Sum(x => x.b.BaseAmount + x.b.TaxAmount),
                            AvgPerConsumer = g.Average(x => x.b.BaseAmount + x.b.TaxAmount),
                            OverdueBills = g.Count(x => x.b.PaymentStatus == "Overdue")
                        };

            return await query.ToListAsync();
        }
    }
}
