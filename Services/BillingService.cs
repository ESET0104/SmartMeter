﻿using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Data.Entities;
using SmartMeterWeb.Interfaces;
using static SmartMeterWeb.Models.Billing.BillingDto;
using Microsoft.EntityFrameworkCore;

namespace SmartMeterWeb.Services
{
    public class BillingService : IBillingService
    {
        private readonly AppDbContext _context;

        public BillingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BillingResponseDto> GenerateMonthlyBillAsync(BillingRequestDto dto)
        {
            var consumer = await _context.Consumers.FirstOrDefaultAsync(c => c.ConsumerId == dto.ConsumerId);
            if (consumer == null) throw new Exception("Consumer not found");

            var meter = await _context.Meters.FirstOrDefaultAsync(m => m.MeterSerialNo == dto.MeterId && m.ConsumerId == dto.ConsumerId);
            if (meter == null) throw new Exception("Meter not found");

            var startDate = DateTime.SpecifyKind(new DateTime(dto.Year, dto.Month, 1), DateTimeKind.Utc);
            var endDate = DateTime.SpecifyKind(startDate.AddMonths(1).AddDays(-1), DateTimeKind.Utc);

            var readings = await _context.MeterReadings
                .Where(r => r.MeterId == meter.MeterSerialNo &&
                            r.ReadingDate >= startDate &&
                            r.ReadingDate <= endDate)
                .ToListAsync();

            if (!readings.Any()) throw new Exception("No readings found");

            var tariff = await _context.Tariffs
                .Where(t => t.EffectiveFrom <= DateOnly.FromDateTime(startDate) &&
                            (t.EffectiveTo == null || t.EffectiveTo >= DateOnly.FromDateTime(endDate)))
                .OrderByDescending(t => t.EffectiveFrom)
                .FirstOrDefaultAsync();

            if (tariff == null) throw new Exception("No applicable tariff found");

            var todRules = await _context.TodRules
                .Where(t => t.TariffId == tariff.TariffId && !t.IsDeleted)
                .ToListAsync();

            var slabs = await _context.TariffSlabs
                .Where(s => s.TariffId == tariff.TariffId && !s.IsDeleted)
                .OrderBy(s => s.FromKwh)
                .ToListAsync();

            decimal totalAmount = 0;

            foreach (var reading in readings)
            {

                var readingTime = DateTime.SpecifyKind(reading.ReadingDate, DateTimeKind.Utc);

                decimal kwh = (decimal)reading.EnergyConsumed;

                // TOD check
                var tod = todRules.FirstOrDefault(t => readingTime.TimeOfDay >= t.StartTime.ToTimeSpan() &&
                                                       readingTime.TimeOfDay < t.EndTime.ToTimeSpan());

                if (tod != null)
                {
                    totalAmount += Math.Round(kwh * tod.RatePerKwh, 2);
                }
                else
                {
                    // Slab calculation
                    decimal remainingKwh = kwh;
                    foreach (var slab in slabs)
                    {
                        if (remainingKwh <= 0) break;

                        decimal slabKwh = Math.Min(remainingKwh, slab.ToKwh - slab.FromKwh);
                        totalAmount += Math.Round(slabKwh * slab.RatePerKwh, 2);
                        remainingKwh -= slabKwh;
                    }
                }
            }

            // Add BaseRate and tax
            totalAmount += tariff.BaseRate;
            decimal taxAmount = Math.Round(totalAmount * tariff.TaxRate / 100, 2);
            totalAmount += taxAmount;

            var bill = new Billing
            {
                ConsumerId = dto.ConsumerId,
                MeterId = dto.MeterId,
                BillingPeriodStart = DateOnly.FromDateTime(startDate),
                BillingPeriodEnd = DateOnly.FromDateTime(endDate),
                TotalUnitsConsumed = (decimal)readings.Sum(r => r.EnergyConsumed),
                BaseAmount = totalAmount - taxAmount,
                TaxAmount = taxAmount,
                TotalAmount = totalAmount,
                GeneratedAt = DateTime.UtcNow, // ✅ UTC
                DueDate = DateOnly.FromDateTime(endDate.AddDays(10))
            };

            _context.Billings.Add(bill);
            await _context.SaveChangesAsync();

            return new BillingResponseDto
            {
                BillId = bill.BillId,
                ConsumerId = bill.ConsumerId,
                MeterId = bill.MeterId,
                TotalUnitsConsumed = bill.TotalUnitsConsumed,
                BaseAmount = bill.BaseAmount,
                TaxAmount = bill.TaxAmount,
                TotalAmount = bill.TotalAmount,
                BillingMonth = $"{dto.Month:D2}-{dto.Year}",
                PaymentStatus = bill.PaymentStatus
            };
        }

        public async Task<IEnumerable<BillingResponseDto>> GetPreviousBillsAsync(long consumerId)
        {
            var bills = await _context.Billings
                .Where(b => b.ConsumerId == consumerId)
                .OrderByDescending(b => b.BillingPeriodStart)
                .Select(b => new BillingResponseDto
                {
                    BillId = b.BillId,
                    ConsumerId = b.ConsumerId,
                    MeterId = b.MeterId,
                    TotalUnitsConsumed = b.TotalUnitsConsumed,
                    BaseAmount = b.BaseAmount,
                    TaxAmount = b.TaxAmount,
                    TotalAmount = b.TotalAmount,
                    BillingMonth = $"{b.BillingPeriodStart:MMM yyyy}",
                    PaymentStatus = b.PaymentStatus
                })
                .ToListAsync();

            return bills;
        }

    }
}
