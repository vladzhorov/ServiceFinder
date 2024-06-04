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
                await HandleException(context, ex, StatusCodes.Status404NotFound);
            }
            catch (ValidationException ex)
            {
                await HandleException(context, ex, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, _errorDefaultStatusCode);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex, int errorCode)
        {
            SetResponseParameters(context, errorCode);
            LogException(context, ex);

            var errorViewModel = new ErrorViewModel
            {
                ErrorCode = errorCode,
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

        private void SetResponseParameters(HttpContext context, int errorCode)
        {
            context.Response.ContentType = ApiConstants.JsonContentType;
            context.Response.StatusCode = errorCode;
        }
    }
}
