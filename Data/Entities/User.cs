using System.ComponentModel.DataAnnotations;

namespace SmartMeterWeb.Data.Entities
{
    public class User
    {
        [Key] public Int64 UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime LastLoginUtc { get; set; }
        public bool IsActive { get; set; }
    }
}
