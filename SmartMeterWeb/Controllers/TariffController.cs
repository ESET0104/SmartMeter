using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeterWeb.Interfaces;
using SmartMeterWeb.Models.UserTarrif;

namespace SmartMeterWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "User")]

    public class TariffController : ControllerBase
    {
        private readonly ITariffService _tariffService;

        public TariffController (ITariffService tariffService)
        {
            _tariffService = tariffService;
        }


        [AllowAnonymous]
        [HttpPut("{tariffId}")]
        public async Task<IActionResult> UpdateTariff(int tariffId, [FromBody] UpdateTariffDto dto)
        {
            var success = await _tariffService.UpdateTariffAsync(tariffId, dto);
            if (!success)
                return NotFound(new { Message = "Tariff not found" });
            return Ok(new { Message = "Tariff updated successfully" });
        }

        [AllowAnonymous]
        [HttpPut("todrule/{todRuleId}")]
        public async Task<IActionResult> UpdateTodRule(int todRuleId, [FromBody] UpdateTodRuleDto dto)
        {
            var success = await _tariffService.UpdateTodRuleAsync(todRuleId, dto);
            if (!success)
                return NotFound(new { Message = "TOD Rule not found" });
            return Ok(new { Message = "TOD Rule updated successfully" });
        }

        [AllowAnonymous]
        [HttpPut("slab/{tariffSlabId}")]
        public async Task<IActionResult> UpdateTariffSlab(int tariffSlabId, [FromBody] UpdateTariffSlabDto dto)
        {
            var success = await _tariffService.UpdateTariffSlabAsync(tariffSlabId, dto);
            if (!success)
                return NotFound(new { Message = "Tariff Slab not found" });
            return Ok(new { Message = "Tariff Slab updated successfully" });
        }

    }
}
