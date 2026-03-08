namespace ComercioApi.Application.DTOs;

public record ComercianteCreateUpdateDto(
    string NombreRazonSocial,
    int MunicipioId,
    string? Telefono,
    string? Correo,
    DateTime FechaRegistro,
    string Estado);
