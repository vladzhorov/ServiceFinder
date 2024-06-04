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
                await HandleException(context, ex, ErrorConstants.ModelNotFoundError);
            }
            catch (FluentValidation.ValidationException ex)
            {
                await HandleException(context, ex, ErrorConstants.ValidationError);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, ErrorConstants.InternalServerError);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex, string errorCode)
        {
            SetResponseParameters(context);
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

        private void SetResponseParameters(HttpContext context)
        {
            context.Response.ContentType = ApiConstants.JsonContentType;
        }
    }
}
