using Microsoft.AspNetCore.Mvc;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Services;

namespace SmartMeterWeb.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ConsumerTariffServiceController : ControllerBase
    {
        private readonly IConsumerTariffService _consumerTariffService;

        public ConsumerTariffServiceController(IConsumerTariffService consumerTariffService)
        {
            _consumerTariffService = consumerTariffService;
        }

       
        [HttpGet("consumer/{consumerId}")]
        public async Task<IActionResult> GetConsumerTariffDetails(long consumerId)
        {
            var data = await _consumerTariffService.GetConsumerTariffDetailsAsync(consumerId);

            if (data == null)
                return NotFound(new { Message = "Consumer not found or inactive" });

            return Ok(data);
        }
    }
}



