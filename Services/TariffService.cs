using Microsoft.EntityFrameworkCore;
using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Models.UserTarrif;


namespace SmartMeterWeb.Services
   
{
    public class TariffService : ITariffService
    {
        private readonly AppDbContext _context;

        public TariffService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> UpdateTariffAsync(int tariffId, UpdateTariffDto dto)
        {
            var tariff = await _context.Tariffs.FirstOrDefaultAsync(t => t.TariffId == tariffId);
            if (tariff == null)
                return false;

            tariff.Name = dto.Name;
            tariff.BaseRate = dto.BaseRate;
            tariff.TaxRate = dto.TaxRate;
            tariff.EffectiveFrom = dto.EffectiveFrom;
            tariff.EffectiveTo = dto.EffectiveTo;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTodRuleAsync(int todRuleId, UpdateTodRuleDto dto)
        {
            var rule = await _context.TodRules.FirstOrDefaultAsync(t => t.TodRuleId == todRuleId && !t.IsDeleted);
            if (rule == null)
                return false;

            rule.Name = dto.Name;
            rule.StartTime = dto.StartTime;
            rule.EndTime = dto.EndTime;
            rule.RatePerKwh = dto.RatePerKwh;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTariffSlabAsync(int tariffSlabId, UpdateTariffSlabDto dto)
        {
            var slab = await _context.TariffSlabs.FirstOrDefaultAsync(t => t.TariffSlabId == tariffSlabId && !t.IsDeleted);
            if (slab == null)
                return false;

            slab.FromKwh = dto.FromKwh;
            slab.ToKwh = dto.ToKwh;
            slab.RatePerKwh = dto.RatePerKwh;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
