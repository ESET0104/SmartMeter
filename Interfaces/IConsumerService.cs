using Microsoft.AspNetCore.Mvc;
using SmartMeterWeb.Data.Entities;

namespace SmartMeterWeb.Interfaces
{
    public interface IConsumerService
    {
        Task<ConsumerDto> GetConsumerDetailsAsync(string Name);
        Task<IActionResult> UploadConsumerPhotoAsync(string Name, IFormFile file);
    }
}
