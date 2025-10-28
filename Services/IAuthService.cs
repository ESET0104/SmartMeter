using SmartMeterWeb.Models.Auth;

namespace SmartMeterWeb.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto Request);
        //Task<AuthResponseDto> RegisterAsync(RegisterRequestDto Request);
    }
}
