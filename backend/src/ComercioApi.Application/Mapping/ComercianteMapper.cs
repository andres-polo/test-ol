using ComercioApi.Application.DTOs;
using ComercioApi.Core.Entities;

namespace ComercioApi.Application.Mapping;

public static class ComercianteMapper
{
    public static ComercianteDto ToDto(Comerciante c) => new(
        c.Id,
        c.NombreRazonSocial,
        c.Municipio.Nombre,
        c.Telefono,
        c.Correo,
        c.FechaRegistro,
        c.Estado,
        c.Establecimientos.Count,
        c.Establecimientos.Sum(e => e.Ingresos),
        c.Establecimientos.Sum(e => e.NumeroEmpleados));
}
