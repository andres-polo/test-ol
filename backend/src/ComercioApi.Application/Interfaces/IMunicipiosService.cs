using ComercioApi.Application.DTOs;

namespace ComercioApi.Application.Interfaces;

public interface IMunicipiosService
{
    Task<IReadOnlyList<MunicipioDto>> GetAllAsync(CancellationToken ct = default);
}
