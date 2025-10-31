﻿using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Data.Entities;
using SmartMeterWeb.Interfaces;
using Microsoft.EntityFrameworkCore;
using SmartMeterWeb.Models;


namespace SmartMeterWeb.Services
{
    public class CustomerCareService : ICustomerCareService
    {
        private readonly AppDbContext _context;

       // private readonly IMailService _mailService;

        public CustomerCareService(AppDbContext context)
        {
            _context = context;
            
        }

        public async Task AddMessageAsync(CustomerCareDto dto)
        {
            var message = new CustomerCareMessage
            {
                ConsumerId = dto.ConsumerId,
                Name = dto.Name,
                Phone = dto.PhoneNumber,
                Message = dto.Message
            };
            //await _mailService.SendEmailAsync(
            //    "msurendra.nitw@gmail.com", "your issue will be resolved", "it will take in 3 days"
            //    );
            _context.CustomerCareMessages.Add(message);
            await _context.SaveChangesAsync();
            

            
                
        }

        public async Task<List<CustomerCareMessage>> GetAllMessagesAsync()
        {
            return await _context.CustomerCareMessages
                .OrderByDescending(m => m.MessageId)
                .ToListAsync();
        }


    }
}
