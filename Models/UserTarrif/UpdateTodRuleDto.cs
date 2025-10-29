namespace SmartMeterWeb.Models.UserTarrif
{
    public class UpdateTodRuleDto
    {
        public string Name { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal RatePerKwh { get; set; }
    }
}
