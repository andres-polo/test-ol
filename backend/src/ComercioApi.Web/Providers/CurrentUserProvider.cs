using System.Security.Claims;
using ComercioApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ComercioApi.Web.Providers;

/// <summary>
/// Implementación que obtiene el nombre del usuario desde los claims del HttpContext (JWT).
/// </summary>
public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserName() =>
        _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value
        ?? _httpContextAccessor.HttpContext?.User?.FindFirst("name")?.Value
        ?? "Sistema";
}
