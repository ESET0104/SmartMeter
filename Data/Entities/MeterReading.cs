
using System.ComponentModel.DataAnnotations;

namespace SmartMeterWeb.Data.Entities
{
    public class MeterReading
    {
        [Key] public long MeterReadingId { get; set; }
        public string MeterId { get; set; } = null!;
        public Meter Meter { get; set; } = null!;
        public DateTimeOffset MeterReadingDate { get; set; }
        public decimal EnergyConsumed { get; set; }
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
    }
}
