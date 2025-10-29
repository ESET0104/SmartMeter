namespace SmartMeterWeb.Models.Tariffs
{
    public class TariffSlabDto
    {
        public decimal FromKwh { get; set; }
        public decimal ToKwh { get; set; }
        public decimal RatePerKwh { get; set; }
    }
}
