using SmartMeter.Data.Entities;

namespace SmartMeterWeb.Data.Entities
{
    public class TariffDetails
    {
        public int TariffDetailsId { get; set; }
        public int? TariffSlabId { get; set; }
        public TariffSlab? TariffSlab { get; set; }
        public int? TodRuleId { get; set; }
        public TodRule? TodRule { get; set; }
    }
}
