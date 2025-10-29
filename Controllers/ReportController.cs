using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeterWeb.Services;

namespace SmartMeterWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class ReportController:ControllerBase
    {
        private readonly ReportService _reportService;
        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("monthly-tariff")]
        public async Task<IActionResult> GetMonthlyTariffReport([FromQuery] int year, [FromQuery] int month)
        {
            if (year < 2000 || month < 1 || month > 12)
                return BadRequest("Invalid year or month.");

            var fromDate = new DateTime(year, month, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);

            var report = await _reportService.GetMonthlyTariffReportAsync(fromDate, toDate);
            return Ok(report);
        }

    }

}
