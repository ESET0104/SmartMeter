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
        private readonly IMailService _mailService;

        public CustomerCareController(AppDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        [AllowAnonymous]
        [HttpPost("send")]
       
        public async Task AddMessageAsync(CustomerCareMessage message)
        {
            _context.CustomerCareMessages.Add(message);
            await _context.SaveChangesAsync();
            await _mailService.SendEmailAsync(
                "msurendranitw@gmail.com",
                "Customer Care",
                "<p>Your issue has been received. We will resolve it within 3 days.</p>"
            );

        }
        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<List<CustomerCareMessage>> GetAllMessagesAsync()
        {
            return await _context.CustomerCareMessages
                .OrderByDescending(m => m.MessageId)
                .ToListAsync();

        }
        //[AllowAnonymous]
        //[HttpGet("test-email")]
        //public async Task<IActionResult> TestEmail()
        //{
        //    await _mailService.SendEmailAsync(
        //        "msurendranitw@gmail.com",
        //        "Customer Issue Received",
        //        "<p>Your issue has been received. We will resolve it within 3 days.</p>"
        //    );

        //    return Ok("Email sent!");
        //}

    }
}
