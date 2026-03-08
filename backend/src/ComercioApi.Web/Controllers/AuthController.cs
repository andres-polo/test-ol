using ComercioApi.Application.DTOs;
using ComercioApi.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioApi.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public AuthController(IAuthService authService, IJwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), 200)]
    [ProducesResponseType(typeof(ApiResponse<object>), 401)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var result = await _authService.ValidateCredentialsAsync(request.Correo, request.Contrasena, ct);
        if (result == null)
            return Ok(ApiResponse.Error("Credenciales inválidas"));

        var token = _jwtService.GenerateToken(result.UserId, result.Nombre, result.Correo, result.Rol);
        var expiraEn = DateTime.UtcNow.AddHours(1);

        return Ok(ApiResponse.Ok(new LoginResponse(token, result.Nombre, result.Rol, expiraEn)));
    }
}
