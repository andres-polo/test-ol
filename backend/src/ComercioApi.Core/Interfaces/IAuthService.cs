namespace ComercioApi.Core.Interfaces;

public interface IAuthService
{
    Task<LoginResult?> ValidateCredentialsAsync(string correo, string password, CancellationToken cancellationToken = default);
}

public record LoginResult(int UserId, string Nombre, string Correo, string Rol);
