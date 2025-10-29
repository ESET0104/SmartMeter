using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Models.Reports;
using Microsoft.EntityFrameworkCore;

namespace SmartMeterWeb.Services
{
//    public class ConsumptionReportService : IConsumptionReportService
//    {
//        private readonly AppDbContext _context;
//        public ConsumptionReportService(AppDbContext context)
//        {
//            _context = context;
//        }

//        //public async Task<HistoricalConsumptionResponseDto> GetHistoricalConsumptionAsync(HistoricalConsumptionRequestDto request)
//        //{
//        //    var fromDateTime = request.FromDate.ToDateTime(TimeOnly.MinValue);
//        //    var toDateTime = request.ToDate.ToDateTime(TimeOnly.MaxValue);

//        //    // Manual join query
//        //    var query =
//        //        from org in _context.OrgUnits
//        //        join con in _context.Consumers on org.OrgUnitId equals con.OrgUnitId
//        //        join meter in _context.Meters on con.ConsumerId equals meter.ConsumerId
//        //        join reading in _context.MeterReadings on meter.MeterSerialNo equals reading.MeterId
//        //        where org.OrgUnitId == request.OrgUnitId
//        //              && readings.MeterReadingDate >= fromDateTime
//        //              && readings.MeterReadingDate <= toDateTime
//        //        group reading by new { org.OrgUnitId, org.Name } into g
//        //        select new HistoricalConsumptionResponseDto
//        //        {
//        //            OrgUnitId = g.Key.OrgUnitId,
//        //            OrgUnitName = g.Key.Name,
//        //            TotalEnergyConsumed = g.Sum(x => x.EnergyConsumed),
//        //            TotalMeters = g.Select(x => x.MeterId).Distinct().Count(),
//        //            TotalConsumers = (
//        //                from c in _context.Consumers
//        //                where c.OrgUnitId == g.Key.OrgUnitId
//        //                select c.ConsumerId
//        //            ).Count()
//                };

//           // return await query.FirstOrDefaultAsync() ?? new HistoricalConsumptionResponseDto();
    //    }
   // }
}
