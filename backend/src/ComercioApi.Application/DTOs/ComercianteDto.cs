namespace ComercioApi.Application.DTOs;

public record ComercianteDto(
    int Id,
    string NombreRazonSocial,
    string Municipio,
    string? Telefono,
    string? Correo,
    DateTime FechaRegistro,
    string Estado,
    int CantidadEstablecimientos,
    decimal TotalIngresos,
    int CantidadEmpleados);
