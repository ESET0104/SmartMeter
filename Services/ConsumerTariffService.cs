using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Models.Tariffs;
using Microsoft.EntityFrameworkCore;

namespace SmartMeterWeb.Services
{
    public class ConsumerTariffService : IConsumerTariffService
    {
        private readonly AppDbContext _context;

        public ConsumerTariffService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TariffInfoDto?> GetConsumerTariffDetailsAsync(long consumerId)
        {
            // Step 1: Get Consumer’s TariffId
            var consumerTariff = await (from c in _context.Consumers
                                        where c.ConsumerId == consumerId
                                        select new { c.TariffId }).FirstOrDefaultAsync();

            if (consumerTariff == null)
                return null;

            var tariffId = consumerTariff.TariffId;

            // Step 2: Get Tariff Info
            var tariffInfo = await (from t in _context.Tariffs
                                    where t.TariffId == tariffId
                                    select new TariffInfoDto
                                    {
                                        TariffName = t.Name,
                                        BaseRate = t.BaseRate,
                                        TaxRate = t.TaxRate,
                                        TodRules = new List<TodRuleDto>(),
                                        TariffSlabs = new List<TariffSlabDto>()
                                    }).FirstOrDefaultAsync();

            if (tariffInfo == null)
                return null;

            // Step 3: TOD Rules (manual join)
            tariffInfo.TodRules = await (from tr in _context.TodRules
                                         where tr.TariffId == tariffId && !tr.IsDeleted
                                         orderby tr.StartTime
                                         select new TodRuleDto
                                         {
                                             Name = tr.Name,
                                             StartTime = tr.StartTime,
                                             EndTime = tr.EndTime,
                                             RatePerKwh = tr.RatePerKwh
                                         }).ToListAsync();

            // Step 4: Tariff Slabs (manual join)
            tariffInfo.TariffSlabs = await (from ts in _context.TariffSlabs
                                            where ts.TariffId == tariffId && !ts.IsDeleted
                                            orderby ts.FromKwh
                                            select new TariffSlabDto
                                            {
                                                FromKwh = ts.FromKwh,
                                                ToKwh = ts.ToKwh,
                                                RatePerKwh = ts.RatePerKwh
                                            }).ToListAsync();

            return tariffInfo;
        }
    }
}
