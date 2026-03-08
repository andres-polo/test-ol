using ComercioApi.Core.Entities;

namespace ComercioApi.Core.Interfaces;

public interface IMunicipioRepository
{
    Task<IReadOnlyList<Municipio>> GetAllAsync(CancellationToken ct = default);
}
