using System.Net;
using System.Text.Json;
using ComercioApi.Application.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ComercioApi.Web.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "Error no controlado: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message, errors) = exception switch
        {
            ArgumentException arg => (HttpStatusCode.BadRequest, arg.Message, (IReadOnlyList<string>?)null),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "No autorizado", null),
            KeyNotFoundException => (HttpStatusCode.NotFound, "Recurso no encontrado", null),
            DbUpdateException dbEx when dbEx.InnerException is SqlException => 
                (HttpStatusCode.BadRequest, "Error de base de datos", null),
            DbUpdateException => (HttpStatusCode.BadRequest, "Error al guardar los datos", null),
            _ => (HttpStatusCode.InternalServerError, "Ha ocurrido un error interno", null)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = ApiResponse.Error(message, errors);
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}
