namespace SmartMeterWeb.Models.Reports
{
    public class MonthlyTariffReportDto
    {
        public string TariffName { get; set; } = string.Empty;
        public int TotalConsumers { get; set; }
        public decimal TotalUnits { get; set; }
        public decimal BaseRevenue { get; set; }
        public decimal TaxCollected { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AvgPerConsumer { get; set; }
        public int OverdueBills { get; set; }
    }


}

