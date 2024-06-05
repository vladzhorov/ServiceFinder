using ServiceFinder.API.Constants;
using ServiceFinder.API.ViewModels;
using ServiceFinder.BLL.Exceptions;
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
                await HandleException(context, ex, ErrorCodesConstants.ModelNotFoundErrorCode, StatusCodes.Status404NotFound);
            }
            catch (FluentValidation.ValidationException ex)
            {
                await HandleException(context, ex, ErrorCodesConstants.ValidationErrorCode, StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, ErrorCodesConstants.InternalServerErrorCode, _errorDefaultStatusCode);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex, string errorCode, int statusCode)
        {
            SetResponseParameters(context, statusCode);
            LogException(context, ex);

            var errorViewModel = new ErrorViewModel
            {
                ErrorCode = errorCode,
                Message = ex.Message
            };

            var errorJson = JsonSerializer.Serialize(errorViewModel);
            await context.Response.WriteAsync(errorJson);
        }

        private void LogException(HttpContext context, Exception ex)
        {
            _logger.LogWarning(ex, $"{ex.Message}");
            _logger.LogWarning(ex, $"Exception in query: {context?.Request.Path}");
        }

        private void SetResponseParameters(HttpContext context, int statusCode)
        {
            context.Response.ContentType = ApiConstants.JsonContentType;
            context.Response.StatusCode = statusCode;
        }
    }
}
