using Microsoft.EntityFrameworkCore;
using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Data.Entities;
using SmartMeterWeb.Models.Auth;
using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SmartMeterWeb.Services
{
    public class AuthService : IAuthService
    {
        public readonly AppDbContext _context;
        public readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            if(request.Role == "User")
            {
                if(await _context.Users.AnyAsync(u => u.UserName == request.UserName))
                {
                    throw new Exception("UserName already taken");
                }

                var user = new User
                {
                    UserName = request.UserName,
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    Phone = request.Phone,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    IsActive = true
                };

                _context.Users.Add(user);
                
            }
            else if(request.Role == "Consumer")
            {
                if(await _context.Consumers.AnyAsync(c => c.Email == request.Email))
                {
                    throw new Exception("Email already registered.");
                }

                var consumer = new Consumer
                {
                    Name = request.UserName,
                    Email = request.Email,
                    Phone = request.Phone,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    Status = "Active",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };

                _context.Consumers.Add(consumer);
            }
            else
            {
                throw new Exception("invalid role");
            }

            await _context.SaveChangesAsync();

            return new AuthResponseDto { Name = request.DisplayName ?? request.UserName, Role = request.Role };
        }

        //public async Task<AuthResponseDto> LoginAsync(RegisterRequestDto request)
        //{

        //}
    }
}
