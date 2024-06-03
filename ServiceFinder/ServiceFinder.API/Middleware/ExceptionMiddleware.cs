using ServiceFinder.API.Constants;
using ServiceFinder.API.Exceptions;
using ServiceFinder.API.Validators;
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
                context.Response.StatusCode = ex.StatusCode;
                await HandleException(context, ex);

            }
            catch (FluentValidatorException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await HandleException(context, ex);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = _errorDefaultStatusCode;
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            SetResponseParameters(context);
            LogException(context, ex);
            var errorViewModel = new ErrorViewModel
            {
                StatusCode = context.Response.StatusCode,
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

        private void SetResponseParameters(HttpContext context)
        {
            context.Response.ContentType = ApiRoutes.ApplicationJson;
        }
    }
}
