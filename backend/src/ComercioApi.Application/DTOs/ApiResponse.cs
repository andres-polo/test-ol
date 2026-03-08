namespace ComercioApi.Application.DTOs;

public record ApiResponse<T>(bool Success, T? Data, string? Message, IReadOnlyList<string>? Errors);

public static class ApiResponse
{
    public static ApiResponse<T> Ok<T>(T data, string? message = null) =>
        new(true, data, message, null);

    public static ApiResponse<object> Error(string message, IReadOnlyList<string>? errors = null) =>
        new(false, null, message, errors);
}
