using ComercioApi.Application.DTOs;

namespace ComercioApi.Application.Interfaces;

public interface IComerciantesService
{
    Task<PagedResult<ComercianteDto>> GetPagedAsync(int page, int pageSize, string? nombre, DateTime? fechaDesde, DateTime? fechaHasta, string? estado, CancellationToken ct = default);
    Task<ComercianteDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ComercianteDto> CreateAsync(ComercianteCreateUpdateDto dto, string usuarioModifica, CancellationToken ct = default);
    Task<ComercianteDto?> UpdateAsync(int id, ComercianteCreateUpdateDto dto, string usuarioModifica, CancellationToken ct = default);
    Task<ComercianteDto?> PatchEstadoAsync(int id, string estado, string usuarioModifica, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<byte[]> GetReporteCsvAsync(CancellationToken ct = default);
}
