namespace SmartMeterWeb.Models.Tariffs
{
    public class TariffInfoDto
    {
        public string TariffName { get; set; }
        public decimal BaseRate { get; set; }
        public decimal TaxRate { get; set; }
        public List<TodRuleDto> TodRules { get; set; }
        public List<TariffSlabDto> TariffSlabs { get; set; }
    }
}
