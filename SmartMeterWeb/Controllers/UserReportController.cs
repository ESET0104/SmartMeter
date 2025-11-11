using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Models.Reports;

namespace SmartMeterWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]// for User
    public class UserReportController :ControllerBase
    {
        private readonly IUserReportService _userReportService;

        public UserReportController(IUserReportService userReportService)
        {
            _userReportService = userReportService;
        }

        [AllowAnonymous]
        [HttpGet("daily-consumption")]
        public async Task<ActionResult<List<HistoricalConsumptionDto>>> GetDailyConsumption(
            [FromQuery] DateTime date, [FromQuery] int? orgUnitId = null)
        {
            try
            {
                var request = new HistoricalConsumptionRequestDto
                {
                    Date = date,
                    OrgUnitId = orgUnitId
                };

                var result = await _userReportService.GetHistoricalConsumptionAsync(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

    }
}
