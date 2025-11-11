using SmartMeterWeb.Exceptions;
using System.Net;
using System.Text.Json;

namespace SmartMeterWeb.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                _logger.LogWarning($"API ERROR: {ex.Message}");
                await HandleExceptionAsync(context, ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error occurred");
                await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var errorResponse = new
            {
                statusCode,
                message,
                time = DateTime.UtcNow
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
