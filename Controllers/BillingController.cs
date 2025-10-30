using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeterWeb.Interfaces;
using static SmartMeterWeb.Models.Billing.BillingDto;

namespace SmartMeterWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpPost("Generate")]
        public async Task<ActionResult<BillingResponseDto>> GenerateMonthlyBill([FromBody] BillingRequestDto dto)
        {
            try
            {
                var bill = await _billingService.GenerateMonthlyBillAsync(dto);
                return Ok(bill);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("Previous/{consumerId}")]
        public async Task<ActionResult<IEnumerable<BillingResponseDto>>> GetPreviousBills(long consumerId)
        {
            try
            {
                var bills = await _billingService.GetPreviousBillsAsync(consumerId);
                return Ok(bills);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
