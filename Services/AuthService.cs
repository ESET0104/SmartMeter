using Microsoft.EntityFrameworkCore;
using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Data.Entities;
using SmartMeterWeb.Models.AuthDto;
//using BCrypt.Net;
//using Microsoft.AspNetCore.Http.HttpResults;
//using System.CodeDom.Compiler;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Diagnostics.Eventing.Reader;
//using System.Security.Claims;

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

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            if(request.Role == "User")
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UsernameOrEmail);
                if(user == null)
                {
                    throw new Exception("Invalid credentials");
                }
                else if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    throw new Exception("Wrong Password");
                }

                var token = GenerateJwtToken(user.UserName, "User");
                user.LastLoginUtc = DateTime.UtcNow;
                return new AuthResponseDto { Name= user.UserName, Role = "User", Token = token };
            }
            else if (request.Role == "Consumer")
            {
                var user = await _context.Consumers.FirstOrDefaultAsync(u => u.Email == request.UsernameOrEmail);
                if (user == null || BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    throw new Exception("Invalid credentials");
                }

                var token = GenerateJwtToken(user.Email, "Consumer");
                
                return new AuthResponseDto { Name = user.Name, Role = "Consumer", Token = token };
            }
            else
            {
                throw new Exception("Invalid Role");
            }
        }

        private string GenerateJwtToken(string username, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
