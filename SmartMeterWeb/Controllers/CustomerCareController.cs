using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Data.Entities;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Models;
using SmartMeterWeb.Services;
using System.Collections.Generic;
namespace SmartMeterWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class CustomerCareController: BaseController
    {
        private readonly AppDbContext _context;
        private readonly IMailService _mailService;
        private readonly ICustomerCareService _customerCareService;

        public CustomerCareController(AppDbContext context, IMailService mailService, ICustomerCareService customerCareService)
        {
            _context = context;
            _mailService = mailService;
            _customerCareService = customerCareService;

        }

        [AllowAnonymous]
        [HttpPost("send")]

        public async Task<IActionResult> AddMessageAsync([FromBody] CustomerCareDto dto)
        {
            try
            {
                await _customerCareService.AddMessageAsync(dto);
                return Success<object>(null, "Your message has been received. We’ll respond within 3 days.");
            }
            catch (Exception ex)
            {
                return Error($"Failed to submit message: {ex.Message}", 500);
            }
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllMessagesAsync()
        {
            try
            {
                var messages = await _customerCareService.GetAllMessagesAsync();

                if (messages == null || !messages.Any())
                    return Error("No messages found", 404);

                return Success(messages, "Messages fetched successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Failed to fetch messages: {ex.Message}", 500);
            }

        }

        [AllowAnonymous]
        [HttpPost("reply")]
        public async Task<IActionResult> SendReplyToCustomer([FromBody] CustomerReplyDto dto)
        {
            try
            {
                await _customerCareService.SendReplyToCustomer(dto);
                return Success<object>(null, "Reply sent to the customer successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Failed to send reply: {ex.Message}", 500);
            }
        }


    }
}
