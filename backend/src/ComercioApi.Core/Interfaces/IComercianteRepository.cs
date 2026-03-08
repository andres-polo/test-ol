using ComercioApi.Core.Entities;

namespace ComercioApi.Core.Interfaces;

public interface IComercianteRepository
{
    Task<Comerciante?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<(IReadOnlyList<Comerciante> Items, int Total)> GetPagedAsync(int page, int pageSize, string? nombre, DateTime? fechaDesde, DateTime? fechaHasta, string? estado, CancellationToken ct = default);
    Task<Comerciante> AddAsync(Comerciante comerciante, CancellationToken ct = default);
    Task UpdateAsync(Comerciante comerciante, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsAsync(int id, CancellationToken ct = default);
}
