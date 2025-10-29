using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Models.Reports;
using Microsoft.EntityFrameworkCore;

namespace SmartMeterWeb.Services
{
    public class ConsumptionReportService : IConsumptionReportService
    {
        private readonly AppDbContext _context;
        public ConsumptionReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HistoricalConsumptionResponseDto> GetHistoricalConsumptionAsync(HistoricalConsumptionRequestDto request)
        {
            var fromDateTime = request.FromDate.ToDateTime(TimeOnly.MinValue);
            var toDateTime = request.ToDate.ToDateTime(TimeOnly.MaxValue);

            // Manual join query
            var query =
                from org in _context.OrgUnit
                join con in _context.Consumer on org.OrgUnitId equals con.OrgUnitId
                join meter in _context.Meter on con.ConsumerId equals meter.ConsumerId
                join reading in _context.MeterReading on meter.MeterSerialNo equals reading.MeterId
                where org.OrgUnitId == request.OrgUnitId
                      && reading.MeterReadingDate >= fromDateTime
                      && reading.MeterReadingDate <= toDateTime
                group reading by new { org.OrgUnitId, org.Name } into g
                select new HistoricalConsumptionResponseDto
                {
                    OrgUnitId = g.Key.OrgUnitId,
                    OrgUnitName = g.Key.Name,
                    TotalEnergyConsumed = g.Sum(x => x.EnergyConsumed),
                    TotalMeters = g.Select(x => x.MeterId).Distinct().Count(),
                    TotalConsumers = (
                        from c in _context.Consumer
                        where c.OrgUnitId == g.Key.OrgUnitId
                        select c.ConsumerId
                    ).Count()
                };

            return await query.FirstOrDefaultAsync() ?? new HistoricalConsumptionResponseDto();
        }
    }
}
