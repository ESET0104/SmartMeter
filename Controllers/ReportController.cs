using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Services;

namespace SmartMeterWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]//User
    public class ReportController:ControllerBase
    {
        private readonly IMonthlyTariffReportService _reportService;

        public ReportController(IMonthlyTariffReportService reportService)
        {
            _reportService = reportService;
        }
        [AllowAnonymous]
        [HttpGet("monthly-tariff")]
        public async Task<IActionResult> GetMonthlyTariffReport([FromQuery] int year, [FromQuery] int month)
        {
            var result = await _reportService.GetMonthlyTariffReportAsync(year, month);
            return Ok(result);
        }


    }

}
