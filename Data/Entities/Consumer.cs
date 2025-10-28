using System.ComponentModel.DataAnnotations;

namespace SmartMeterWeb.Data.Entities
{
    public class Consumer
    {
        public Int64 ConsumerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int OrgUnitId { get; set; }
        public int TariffId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string PasswordHash { get; set; }

    }
}
