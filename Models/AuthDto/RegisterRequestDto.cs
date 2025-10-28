namespace SmartMeterWeb.Models.AuthDto
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; }      // For both
        public string? DisplayName { get; set; }  // For User
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        
    }
}
