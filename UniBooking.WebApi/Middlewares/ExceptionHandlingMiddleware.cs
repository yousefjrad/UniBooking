using Microsoft.AspNetCore.Mvc;
using UniBooking.Domain.Exceptions;

namespace UniBooking.WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (status, title) = ex switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "غير موجود"),
            ConflictException => (StatusCodes.Status409Conflict, "تعارض"),
            BadRequestException => (StatusCodes.Status400BadRequest, "طلب غير صالح"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "غير مصرح"),
            _ => (StatusCodes.Status500InternalServerError, "خطأ داخلي")
        };

        if (status == StatusCodes.Status500InternalServerError)
            _logger.LogError(ex, "Unhandled exception");

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = status == StatusCodes.Status500InternalServerError
                ? "حدث خطأ غير متوقع"
                : ex.Message
        };

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(problem);
    }
}