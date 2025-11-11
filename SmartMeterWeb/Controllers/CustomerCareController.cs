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
    public class CustomerCareController: ControllerBase
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
       
        public async Task AddMessageAsync(CustomerCareMessage message)
        {
            _context.CustomerCareMessages.Add(message);
            await _context.SaveChangesAsync();
            await _mailService.SendEmailAsync(
                message.mailid,
                "Customer Care",
                "<p>Your issue has been received. We will resolve it within 3 days.</p>"
            );
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<List<CustomerCareMessage>> GetAllMessagesAsync()
        {
            Task<List<CustomerCareMessage>> messages = _customerCareService.GetAllMessagesAsync();
            return await messages;

        }

        [AllowAnonymous]
        [HttpPost("reply")]
        public async Task SendReplyToCustomer(CustomerReplyDto dto)
        {
            await _customerCareService.SendReplyToCustomer(dto);
            
        }


    }
}
