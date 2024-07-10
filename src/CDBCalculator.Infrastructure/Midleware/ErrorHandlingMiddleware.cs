using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace CDBCalculator.Infrastructure.Midleware
{
    public class ErrorHandlingMiddleware(RequestDelegate _next, ILogger<ErrorHandlingMiddleware> _logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var message = "Ocorreu um erro inesperado. Tente novamente mais tarde.";
                _logger.LogError(ex, message);
                await HandleExceptionAsync(context, ex, message);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception,string  message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new { error = message });
            return context.Response.WriteAsync(result);
        }
    }
}