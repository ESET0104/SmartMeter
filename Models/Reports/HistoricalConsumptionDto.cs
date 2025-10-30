namespace SmartMeterWeb.Models.Reports
{
    public class HistoricalConsumptionDto
    {
        public DateTime Date { get; set; }
        public string OrgUnitName { get; set; } = string.Empty;
        public string OrgUnitType { get; set; } = string.Empty;
        public decimal TotalEnergyConsumed { get; set; }
        public int ConsumerCount { get; set; }
        public decimal AverageConsumption { get; set; }
        public decimal PeakConsumption { get; set; }
        public decimal LowConsumption { get; set; }
    }

    public class HistoricalConsumptionRequestDto
    {
        public DateTime Date { get; set; }
        public int? OrgUnitId { get; set; }
    }

}
