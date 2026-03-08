namespace ComercioApi.Application.Interfaces;

/// <summary>
/// Provee el nombre del usuario autenticado en el contexto actual (HTTP).
/// Reutilizable en controladores y servicios que necesiten auditoría o trazabilidad.
/// </summary>
public interface ICurrentUserProvider
{
    string GetCurrentUserName();
}
