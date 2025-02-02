using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    // The RequestDelegate is injected by ASP.NET Core's middleware pipeline
    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Continue the request pipeline
            await _next(context);
        }
        catch (ValidationException ex)
        {
            // Handle validation exception
            _logger.LogError($"Validation failed: {string.Join(", ", ex.Errors.Select(e => e.ErrorMessage))}");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            var response = new { Message = "Validation failed", Errors = ex.Errors.Select(e => e.ErrorMessage) };
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            // Handle any other exception
            _logger.LogError($"An unexpected error occurred: {ex.Message}");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new { Message = "An unexpected error occurred in the server Hehe" };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
