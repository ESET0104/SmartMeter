using static SmartMeterWeb.Models.Billing.BillingDto;

namespace SmartMeterWeb.Interfaces
{
    public interface IBillingService
    {
        Task<BillingResponseDto> GenerateMonthlyBillAsync(BillingRequestDto dto);
        Task<IEnumerable<BillingResponseDto>> GetPreviousBillsAsync(long consumerId);
    }
}
