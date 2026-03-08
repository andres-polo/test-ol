using ComercioApi.Core.Interfaces;
using ComercioApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComercioApi.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly ComercioDbContext _context;

    public AuthService(ComercioDbContext context)
    {
        _context = context;
    }

    public async Task<LoginResult?> ValidateCredentialsAsync(string correo, string password, CancellationToken cancellationToken = default)
    {
        var usuario = await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Correo == correo, cancellationToken);

        if (usuario == null) return null;

        var isValid = BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);
        if (!isValid) return null;

        return new LoginResult(usuario.Id, usuario.Nombre, usuario.Correo, usuario.Rol);
    }
}
