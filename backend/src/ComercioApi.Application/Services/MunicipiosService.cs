using ComercioApi.Application.DTOs;
using ComercioApi.Application.Interfaces;
using ComercioApi.Core.Interfaces;

namespace ComercioApi.Application.Services;

public class MunicipiosService : IMunicipiosService
{
    private readonly IMunicipioRepository _repository;

    public MunicipiosService(IMunicipioRepository repository) => _repository = repository;

    public async Task<IReadOnlyList<MunicipioDto>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await _repository.GetAllAsync(ct);
        return entities.Select(m => new MunicipioDto(m.Id, m.Nombre)).ToList();
    }
}
