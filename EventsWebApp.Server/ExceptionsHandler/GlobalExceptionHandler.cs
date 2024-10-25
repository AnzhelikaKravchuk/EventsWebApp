using EventsWebApp.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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

        int status = exception is BadRequestException or ValidationException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;

        var problemDetails = new {
            Status = status,
            Detail = exception.Message,
            Errors = GetErrors(exception)
        };

        httpContext.Response.StatusCode = status;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static List<ValidationFailure> GetErrors(Exception exception)
    {
        List<ValidationFailure> errors = null;
        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors.ToList();
        }
        return errors;
    }
    }

}