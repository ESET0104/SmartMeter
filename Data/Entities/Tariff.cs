
using System.ComponentModel.DataAnnotations;

namespace SmartMeterWeb.Data.Entities
{
    public class Tariff
    {
        [Key] public int TariffId { get; set; }
        public string Name { get; set; } = null!;
        public DateOnly EffectiveFrom { get; set; }
        public DateOnly? EffectiveTo { get; set; }
        public decimal BaseRate { get; set; }
        public decimal TaxRate { get; set; }
    }
}
