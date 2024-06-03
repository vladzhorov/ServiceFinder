using ServiceFinder.API.Constants;
using ServiceFinder.API.ViewModels;
using ServiceFinder.BLL.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ServiceFinder.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly int _errorDefaultStatusCode = StatusCodes.Status500InternalServerError;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ModelNotFoundException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await HandleException(context, ex, ex.StatusCode);

            }
            catch (ValidationException ex)
            {
                await HandleValidationException(context, ex);

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = _errorDefaultStatusCode;
                await HandleException(context, ex, _errorDefaultStatusCode);
            }
        }
        private async Task HandleValidationException(HttpContext context, ValidationException ex)
        {
            SetResponseParameters(context, StatusCodes.Status400BadRequest);
            LogException(context, ex);

            var errorViewModel = new ErrorViewModel
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ex.Message
            };

            var errorJson = JsonSerializer.Serialize(errorViewModel);
            await context.Response.WriteAsync(errorJson);
        }
        private async Task HandleException(HttpContext context, Exception ex, int statusCode)
        {
            SetResponseParameters(context, statusCode);
            LogException(context, ex);
            var errorViewModel = new ErrorViewModel
            {
                StatusCode = statusCode,
                Message = ex.Message
            };
            var errorJson = JsonSerializer.Serialize(errorViewModel);
            await context.Response.WriteAsync(errorJson);
        }

        private void LogException(HttpContext? context, Exception ex)
        {
            _logger.LogWarning(ex, $"{ex.Message}");
            _logger.LogWarning(ex, $"Exception in query: {context?.Request.Path}");
        }

        private void SetResponseParameters(HttpContext context, int statusCode)
        {
            context.Response.ContentType = ApiConstants.ApplicationJson;
            context.Response.StatusCode = statusCode;
        }
    }
}
