using ILogger = Corelibs.Basic.Logging.ILogger;

namespace PageTree.Server.Middlewares
{
    public class ExceptionLogMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionLogMiddleware(RequestDelegate next, ILogger logger)
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
            catch (Exception ex) 
            {
                _logger.Log(ex, "Log Middleware has cought an error!");
                throw ex;
            }
        }
    }

    public static class ExceptionLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionLog(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLogMiddleware>();
        }
    }

}
