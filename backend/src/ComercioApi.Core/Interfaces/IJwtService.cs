namespace ComercioApi.Core.Interfaces;

public interface IJwtService
{
    string GenerateToken(int userId, string nombre, string correo, string rol);
}
