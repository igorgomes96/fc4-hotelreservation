using FC4.HotelReservation.Shared.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FC4.HotelReservation.WebApi;

public class GlobalExceptionHandler: IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case ArgumentException:
                problemDetails.Title = "Validation Error";
                problemDetails.Detail = exception.Message;
                problemDetails.Status = StatusCodes.Status400BadRequest;
                break;
            case InvalidOperationException:
                problemDetails.Title = "Unprocessable Entity";
                problemDetails.Detail = exception.Message;
                problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                break;
            case NotFoundException:
                problemDetails.Title = "Resource Not Found";
                problemDetails.Detail = exception.Message;
                problemDetails.Status = StatusCodes.Status404NotFound;
                break;
            default:
                problemDetails.Title = "Unexpected Error";
                problemDetails.Detail = "An unexpected error occurred.";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                break;
        }

        context.Response.StatusCode = problemDetails.Status.Value;
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

}