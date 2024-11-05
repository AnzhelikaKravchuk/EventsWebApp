namespace EventsWebApp.Server.ExceptionsHandler
{
    public class StatusCodeInterceptorMiddleware
    {
        private readonly RequestDelegate _next;
        public StatusCodeInterceptorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            int status = context.Response.StatusCode;
            switch (status)
            {
                case StatusCodes.Status401Unauthorized:
                    await ExceptionResponseHelper.HandleExceptionResponse(context, status, new Exception("Unauthorized"));
                    return;
                case StatusCodes.Status403Forbidden:
                    await ExceptionResponseHelper.HandleExceptionResponse(context, status, new Exception("Forbidden"));
                    return;
            }

        }
    }
}