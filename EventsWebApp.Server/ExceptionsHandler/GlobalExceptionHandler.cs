using EventsWebApp.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace EventsWebApp.Server.ExceptionsHandler
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

            int status = exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedException or InvalidTokenException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                ConflictException => StatusCodes.Status409Conflict,
                UnprocessableEntityException or ValidationException => StatusCodes.Status422UnprocessableEntity,
                OperationCanceledException => StatusCodes.Status499ClientClosedRequest,
                NotImplementedException => StatusCodes.Status501NotImplemented,
                //For future use
                ServiceNotAvailableException => StatusCodes.Status503ServiceUnavailable,
                OutOfMemoryException => StatusCodes.Status507InsufficientStorage,

                _ => StatusCodes.Status500InternalServerError
            };

            await ExceptionResponseHelper.HandleExceptionResponse(httpContext, status, exception);

            return true;
        }
    }
}