using SmartMeterWeb.Models.AuthDto;

namespace SmartMeterWeb.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto Request);
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto Request);
    }
}
