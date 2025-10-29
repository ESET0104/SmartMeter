using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SmartMeterWeb.Data.Entities;
using System.Globalization;

namespace SmartMeterWeb.Interfaces
{
    public interface IConsumerService
    {
        Task<ConsumerDto> GetConsumerDetailsAsync(string Name);
        Task<IActionResult> UploadConsumerPhotoAsync(string Name, IFormFile file);
    }
}
