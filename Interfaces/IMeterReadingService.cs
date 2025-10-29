using SmartMeterWeb.Data.Entities;
using SmartMeterWeb.Models.AuthDto;

namespace SmartMeterWeb.Interfaces
{
    public interface IMeterReadingService
    {
        Task<MeterReading> RecordReadingAsync(MeterReadingDto dto);
        Task<IEnumerable<MeterReading>> GetReadingsByMeterAsync(string meterId);
    }
}
