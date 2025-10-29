using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMeterWeb.Data.Entities
{
    public class MeterReading
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MeterId { get; set; }

        [ForeignKey(nameof(MeterId))]
        public Meter Meter { get; set; }

        [Required]
        public DateTime ReadingDate { get; set; }

        [Required]
        public double KilowattHours { get; set; }

        [Required]
        public double Voltage { get; set; }

        [Required]
        public double Current { get; set; }

        [Required]
        public double PowerFactor { get; set; }

        [Required]
        public double EnergyConsumed { get; set; }

    }
}
