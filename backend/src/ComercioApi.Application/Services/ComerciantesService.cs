using ComercioApi.Application.DTOs;
using ComercioApi.Application.Interfaces;
using ComercioApi.Application.Mapping;
using ComercioApi.Core.Entities;
using ComercioApi.Core.Interfaces;

namespace ComercioApi.Application.Services;

public class ComerciantesService : IComerciantesService
{
    private readonly IComercianteRepository _repository;
    private readonly IReporteComerciantesService _reporteService;

    public ComerciantesService(IComercianteRepository repository, IReporteComerciantesService reporteService)
    {
        _repository = repository;
        _reporteService = reporteService;
    }

    public async Task<PagedResult<ComercianteDto>> GetPagedAsync(int page, int pageSize, string? nombre, DateTime? fechaDesde, DateTime? fechaHasta, string? estado, CancellationToken ct = default)
    {
        if (pageSize is < 1 or > 100) pageSize = 5;
        var (items, total) = await _repository.GetPagedAsync(page, pageSize, nombre, fechaDesde, fechaHasta, estado, ct);
        var dtos = items.Select(ComercianteMapper.ToDto).ToList();
        return new PagedResult<ComercianteDto>(dtos, total, page, pageSize);
    }

    public async Task<ComercianteDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repository.GetByIdAsync(id, ct);
        return entity is null ? null : ComercianteMapper.ToDto(entity);
    }

    public async Task<ComercianteDto> CreateAsync(ComercianteCreateUpdateDto dto, string usuarioModifica, CancellationToken ct = default)
    {
        var entity = new Comerciante
        {
            NombreRazonSocial = dto.NombreRazonSocial,
            MunicipioId = dto.MunicipioId,
            Telefono = dto.Telefono,
            Correo = dto.Correo,
            FechaRegistro = dto.FechaRegistro,
            Estado = dto.Estado,
            FechaActualizacion = DateTime.UtcNow,
            UsuarioModifica = usuarioModifica
        };
        var created = await _repository.AddAsync(entity, ct);
        var loaded = await _repository.GetByIdAsync(created.Id, ct);
        return ComercianteMapper.ToDto(loaded!);
    }

    public async Task<ComercianteDto?> UpdateAsync(int id, ComercianteCreateUpdateDto dto, string usuarioModifica, CancellationToken ct = default)
    {
        var entity = await _repository.GetByIdAsync(id, ct);
        if (entity is null) return null;

        entity.NombreRazonSocial = dto.NombreRazonSocial;
        entity.MunicipioId = dto.MunicipioId;
        entity.Telefono = dto.Telefono;
        entity.Correo = dto.Correo;
        entity.FechaRegistro = dto.FechaRegistro;
        entity.Estado = dto.Estado;
        entity.UsuarioModifica = usuarioModifica;

        await _repository.UpdateAsync(entity, ct);
        var updated = await _repository.GetByIdAsync(id, ct);
        return ComercianteMapper.ToDto(updated!);
    }

    public async Task<ComercianteDto?> PatchEstadoAsync(int id, string estado, string usuarioModifica, CancellationToken ct = default)
    {
        var entity = await _repository.GetByIdAsync(id, ct);
        if (entity is null) return null;

        entity.Estado = estado;
        entity.UsuarioModifica = usuarioModifica;
        await _repository.UpdateAsync(entity, ct);

        var updated = await _repository.GetByIdAsync(id, ct);
        return ComercianteMapper.ToDto(updated!);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        if (!await _repository.ExistsAsync(id, ct)) return false;
        await _repository.DeleteAsync(id, ct);
        return true;
    }

    public Task<byte[]> GetReporteCsvAsync(CancellationToken ct = default) =>
        _reporteService.GenerarCsvReporteActivosAsync(ct);
}
