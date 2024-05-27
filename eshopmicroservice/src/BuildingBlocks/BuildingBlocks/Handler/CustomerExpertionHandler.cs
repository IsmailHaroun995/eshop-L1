using BuildingBlocks.Exception;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Handler;

internal class CustomerExpertionHandler(ILogger<CustomerExpertionHandler> logger) : IExceptionHandler
{
    public  async ValueTask<bool> TryHandleAsync(HttpContext context, System.Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error Message {Ex} . time to occurrence {time}", exception.Message, DateTime.UtcNow);
        (string Detail, string Title, int StatusCode) details = exception switch
        {
            InternalServerException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
            ),
            ValidationException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            BuildingBlocks.Exception.BadRquestException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            NotFoundException =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status404NotFound
            ),
            _ =>
            (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };
        var problemDeatils = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = context.Request.Path
        };
        problemDeatils.Extensions.Add("tranceId", context.TraceIdentifier);
        if(exception is ValidationException validation)
        {
            problemDeatils.Extensions.Add("validationErrors",
                                          validation.Value);
        }

        await context.Response.WriteAsJsonAsync(problemDeatils, cancellationToken: cancellationToken);
        return true;

    }
}

