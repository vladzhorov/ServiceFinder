using ServiceFinder.BLL.Exceptions;

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
            catch (DomainException ex)
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

        private Task HandleException(HttpContext context, Exception ex)
        {
            SetResponseParameters(context);
            LogException(context, ex);

            return context.Response.WriteAsync($"{ex.Message}\nStatusCode:{context.Response.StatusCode}");
        }

        private void LogException(HttpContext? context, Exception ex)
        {
            _logger.LogWarning(ex, $"{ex.Message}");
            _logger.LogWarning(ex, $"Exception in query: {context?.Request.Path}");
        }

        private void SetResponseParameters(HttpContext context)
        {
            context.Response.ContentType = "application/json";
        }
    }
}
