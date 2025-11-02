using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Data.Entities;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Models.AuthDto;
using System.IdentityModel.Tokens.Jwt;

//using BCrypt.Net;
//using Microsoft.AspNetCore.Http.HttpResults;
//using System.CodeDom.Compiler;
using System.Security.Claims;
using System.Text;
//using System.Security.Claims;

namespace SmartMeterWeb.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogService _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;


        

        public AuthService(AppDbContext context, IConfiguration config, ILogService logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _config = config;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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
                    OrgUnitId = request.OrgUnitId,
                    TariffId = request.TariffId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
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
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            if (request.Role == "User")
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UsernameOrEmail);
                if(user == null)
                {
                    await _logger.LogLoginAttemptAsync(request.UsernameOrEmail, "User", false, "User not found", ipAddress);
                    throw new Exception("User not found");
                }
                else if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    await _logger.LogLoginAttemptAsync(request.UsernameOrEmail, "User", false, "Wrong Password", ipAddress);
                    throw new Exception("Wrong Password");
                }

                await _logger.LogLoginAttemptAsync(request.UsernameOrEmail, "User", true, "Login successful", ipAddress);
                var token = GenerateJwtToken(user.UserName, "User");
                user.LastLoginUtc = DateTime.UtcNow;
                return new AuthResponseDto { Name= user.UserName, Role = "User", Token = token };
            }
            else if (request.Role == "Consumer")
            {
                var user = await _context.Consumers.FirstOrDefaultAsync(u => u.Email == request.UsernameOrEmail);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    await _logger.LogLoginAttemptAsync(request.UsernameOrEmail, "Consumer", false, "Invalid Credentials", ipAddress);
                    throw new Exception("Invalid credentials");
                }

                await _logger.LogLoginAttemptAsync(request.UsernameOrEmail, "Consumer", true, "Login successful", ipAddress);
                var token = GenerateJwtToken(user.Email, "Consumer");
                
                return new AuthResponseDto { Name = user.Name, Role = "Consumer", Token = token };
            }
            else
            {
                await _logger.LogLoginAttemptAsync(request.UsernameOrEmail, request.Role, false, "Invalid Role", ipAddress);
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

        public async Task<ActionResult> UpdatePassWord(PasswordUpdateDto request)
        {
            if(request.role == "User")
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.nameOrEmail);
                if(user == null)
                {
                    throw new Exception("User not found");
                }

                else if (!BCrypt.Net.BCrypt.Verify(request.oldpassword, user.PasswordHash))
                {
                    throw new Exception("Enter the correct old password");
                }

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.newpassword);
                await _context.SaveChangesAsync();
                return new OkObjectResult("password changed successfully");
            }
            else if(request.role =="Consumer")
            {
                var con = await _context.Consumers.FirstOrDefaultAsync(u => u.Email == request.nameOrEmail);
                if (con == null)
                {
                    throw new Exception("User not found");
                }

                else if (!BCrypt.Net.BCrypt.Verify(request.oldpassword, con.PasswordHash))
                {
                    throw new Exception("Enter the correct old password");
                }

                con.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.newpassword);
                await _context.SaveChangesAsync();
                return new OkObjectResult("password changed successfully");
            }
            else
            {
                throw new Exception("wrong role specified");
            }
        }
        
    }
}
