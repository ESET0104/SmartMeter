using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Models.AuthDto;

namespace SmartMeterWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Consumer")]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

        [HttpPost("record")]
        public async Task<IActionResult> RecordReading([FromBody] MeterReadingDto dto)
        {
            var reading = await _meterReadingService.RecordReadingAsync(dto);
            return Ok(reading);
        }

        [HttpGet("{meterId}")]
        public async Task<IActionResult> GetReadings(string meterId)
        {
            var readings = await _meterReadingService.GetReadingsByMeterAsync(meterId);
            return Ok(readings);
        }
    }
}
