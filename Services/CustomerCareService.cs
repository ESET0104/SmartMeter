using SmartMeterWeb.Data.Context;
using SmartMeterWeb.Data.Entities;
using SmartMeterWeb.Interfaces;
using Microsoft.EntityFrameworkCore;
using SmartMeterWeb.Models;


namespace SmartMeterWeb.Services
{
    public class CustomerCareService : ICustomerCareService
    {
        
        private readonly AppDbContext _context;
        private readonly IMailService _mailService;

        public CustomerCareService(AppDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        public async Task AddMessageAsync(CustomerCareDto dto)
        {
            var message = new CustomerCareMessage
            {
                ConsumerId = dto.ConsumerId,
                Name = dto.Name,
                Phone = dto.PhoneNumber,
                Message = dto.Message,
                mailid=dto.mailid
            };

            _context.CustomerCareMessages.Add(message);
            await _context.SaveChangesAsync();
            await _mailService.SendEmailAsync(
        "msurendra.nit@gmail.com",
        "Customer Issue Received",
        "<p>Your issue has been received. We will resolve it within 3 days.</p>"
    );

        }

        public async Task<List<CustomerCareMessage>> GetAllMessagesAsync()
        {
            return await _context.CustomerCareMessages
                .OrderByDescending(m => m.MessageId)
                .ToListAsync();

        }


        public async Task SendReplyToCustomer(CustomerReplyDto dto)
        {
            var reply = new CustomerCareReply
            {
                ResponseId = dto.ResponseId,
                consumerID = dto.consumerID,
                MessageText = dto.MessageText
            };
            await _context.CustomerCareReplies.AddAsync(reply);

            await _context.SaveChangesAsync();

            var consumer = await _context.CustomerCareMessages
                .FirstOrDefaultAsync(c => c.ConsumerId == dto.consumerID);

            if (consumer == null)
                throw new Exception("Consumer not found");

            string customerEmail = consumer.mailid;

            if (string.IsNullOrEmpty(customerEmail))
                throw new Exception("Consumer email not  available");
            

                await _mailService.SendEmailAsync(
        customerEmail,
        "replying to your query",
        dto.MessageText

    );

        }

    }
}
