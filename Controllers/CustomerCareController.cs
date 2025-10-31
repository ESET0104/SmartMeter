using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Data.Entities;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Models;
namespace SmartMeterWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class CustomerCareController: ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerCareController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("send")]
       
        public async Task AddMessageAsync(CustomerCareMessage message)
        {
            _context.CustomerCareMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        [HttpGet("all")]
        public async Task<List<CustomerCareMessage>> GetAllMessagesAsync()
        {
            return await _context.CustomerCareMessages
                .OrderByDescending(m => m.MessageId)
                .ToListAsync();
        }

    }
}
