namespace SmartMeterWeb.Models.UserTarrif
{
    public class UpdateTariffSlabDto
    {
        public decimal FromKwh { get; set; }
        public decimal ToKwh { get; set; }
        public decimal RatePerKwh { get; set; }
    }
}
