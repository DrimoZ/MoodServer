using System.Net;
using Domain.Exception;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Customize the error response based on the exception
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred.";

        // Set the status code and error message based on the exception type
        if (ex is DeletedUserException)
        {
            message = "The selected user is marked as deleted - " + ex.Message;
        }

        // Set the response status code and content
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsJsonAsync(new { error = message });
    }
}