using System.Text;
using ComercioApi.Core.Interfaces;
using ComercioApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComercioApi.Infrastructure.Services;

public class ReporteComerciantesService : IReporteComerciantesService
{
    private readonly ComercioDbContext _context;

    public ReporteComerciantesService(ComercioDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> GenerarCsvReporteActivosAsync(CancellationToken cancellationToken = default)
    {
        var items = await _context.Comerciantes
            .AsNoTracking()
            .Where(c => c.Estado == "Activo")
            .Select(c => new
            {
                c.NombreRazonSocial,
                Municipio = c.Municipio.Nombre,
                c.Telefono,
                c.Correo,
                c.FechaRegistro,
                c.Estado,
                CantidadEstablecimientos = c.Establecimientos.Count,
                TotalIngresos = c.Establecimientos.Sum(e => e.Ingresos),
                CantidadEmpleados = c.Establecimientos.Sum(e => e.NumeroEmpleados)
            })
            .OrderByDescending(x => x.CantidadEstablecimientos)
            .ToListAsync(cancellationToken);

        const char separator = '|';
        var sb = new StringBuilder();
        sb.AppendLine(string.Join(separator, "NombreRazonSocial", "Municipio", "Telefono", "Correo", "FechaRegistro", "Estado", "CantidadEstablecimientos", "TotalIngresos", "CantidadEmpleados"));

        foreach (var item in items)
        {
            sb.AppendLine(string.Join(separator,
                Escape(item.NombreRazonSocial),
                Escape(item.Municipio),
                Escape(item.Telefono ?? ""),
                Escape(item.Correo ?? ""),
                item.FechaRegistro.ToString("yyyy-MM-dd"),
                Escape(item.Estado),
                item.CantidadEstablecimientos,
                item.TotalIngresos.ToString("F2"),
                item.CantidadEmpleados));
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private static string Escape(string value)
    {
        if (value.Contains('|') || value.Contains('"') || value.Contains('\n'))
            return $"\"{value.Replace("\"", "\"\"")}\"";
        return value;
    }
}
