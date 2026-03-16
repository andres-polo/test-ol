using System.Data;
using System.Text;
using ComercioApi.Core.Interfaces;
using ComercioApi.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ComercioApi.Infrastructure.Services;

public class ReporteComerciantesService : IReporteComerciantesService
{
    private readonly ComercioDbContext _context;

    public ReporteComerciantesService(ComercioDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Genera el CSV reutilizando el procedimiento almacenado sp_ReporteComerciantesActivos (Reto 04).
    /// </summary>
    public async Task<byte[]> GenerarCsvReporteActivosAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = (SqlConnection)_context.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        await using var cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "dbo.sp_ReporteComerciantesActivos";

        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        const char separator = '|';
        var sb = new StringBuilder();
        sb.AppendLine(string.Join(separator, "NombreRazonSocial", "Municipio", "Telefono", "Correo", "FechaRegistro", "Estado", "CantidadEstablecimientos", "TotalIngresos", "CantidadEmpleados"));

        while (await reader.ReadAsync(cancellationToken))
        {
            var nombreRazonSocial = reader.GetString(1);
            var municipio = reader.GetString(2);
            var telefono = reader.IsDBNull(3) ? "" : reader.GetString(3);
            var correo = reader.IsDBNull(4) ? "" : reader.GetString(4);
            var fechaRegistro = reader.GetDateTime(5).ToString("yyyy-MM-dd");
            var estado = reader.GetString(6);
            var cantidadEstablecimientos = reader.GetInt32(7);
            var totalIngresos = reader.GetDecimal(8);
            var cantidadEmpleados = reader.GetInt32(9);

            sb.AppendLine(string.Join(separator,
                Escape(nombreRazonSocial),
                Escape(municipio),
                Escape(telefono),
                Escape(correo),
                fechaRegistro,
                Escape(estado),
                cantidadEstablecimientos,
                totalIngresos.ToString("F2"),
                cantidadEmpleados));
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
