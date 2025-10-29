
using System.ComponentModel.DataAnnotations;

namespace SmartMeterWeb.Data.Entities
{
    public class TariffDetails
    {
        [Key] public int TariffDetailsId { get; set; }
        public int? TariffSlabId { get; set; }
        public TariffSlab? TariffSlab { get; set; }
        public int? TodRuleId { get; set; }
        public TodRule? TodRule { get; set; }
    }
}
