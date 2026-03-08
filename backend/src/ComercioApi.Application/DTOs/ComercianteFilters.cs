namespace ComercioApi.Application.DTOs;

public record ComercianteFilters(
    string? NombreRazonSocial,
    DateTime? FechaRegistroDesde,
    DateTime? FechaRegistroHasta,
    string? Estado);
