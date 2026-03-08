using ComercioApi.Core.Entities;
using ComercioApi.Core.Interfaces;
using ComercioApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComercioApi.Infrastructure.Repositories;

public class MunicipioRepository : IMunicipioRepository
{
    private readonly ComercioDbContext _context;

    public MunicipioRepository(ComercioDbContext context) => _context = context;

    public async Task<IReadOnlyList<Municipio>> GetAllAsync(CancellationToken ct = default) =>
        await _context.Municipios.OrderBy(m => m.Nombre).ToListAsync(ct);
}
