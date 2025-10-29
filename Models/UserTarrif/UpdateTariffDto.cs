namespace SmartMeterWeb.Models.UserTarrif
{
    public class UpdateTariffDto
    {
        public string Name { get; set; }
        public decimal BaseRate { get; set; }
        public decimal TaxRate { get; set; }
        public DateOnly EffectiveFrom { get; set; }
        public DateOnly EffectiveTo { get; set; }
    }
}
