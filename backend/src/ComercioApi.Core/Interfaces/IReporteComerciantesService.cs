namespace ComercioApi.Core.Interfaces;

public interface IReporteComerciantesService
{
    Task<byte[]> GenerarCsvReporteActivosAsync(CancellationToken cancellationToken = default);
}
