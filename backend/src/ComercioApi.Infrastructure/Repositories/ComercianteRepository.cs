using ComercioApi.Core.Entities;
using ComercioApi.Core.Interfaces;
using ComercioApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComercioApi.Infrastructure.Repositories;

public class ComercianteRepository : IComercianteRepository
{
    private readonly ComercioDbContext _context;

    public ComercianteRepository(ComercioDbContext context) => _context = context;

    public async Task<Comerciante?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _context.Comerciantes
            .Include(c => c.Municipio)
            .Include(c => c.Establecimientos)
            .FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<(IReadOnlyList<Comerciante> Items, int Total)> GetPagedAsync(
        int page, int pageSize, string? nombre, DateTime? fechaDesde, DateTime? fechaHasta, string? estado,
        CancellationToken ct = default)
    {
        var query = _context.Comerciantes
            .Include(c => c.Municipio)
            .Include(c => c.Establecimientos)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(nombre))
            query = query.Where(c => c.NombreRazonSocial.Contains(nombre));
        if (fechaDesde.HasValue)
            query = query.Where(c => c.FechaRegistro >= fechaDesde.Value);
        if (fechaHasta.HasValue)
            query = query.Where(c => c.FechaRegistro <= fechaHasta.Value);
        if (!string.IsNullOrWhiteSpace(estado))
            query = query.Where(c => c.Estado == estado);

        var total = await query.CountAsync(ct);
        var items = await query
            .OrderByDescending(c => c.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, total);
    }

    public async Task<Comerciante> AddAsync(Comerciante comerciante, CancellationToken ct = default)
    {
        _context.Comerciantes.Add(comerciante);
        await _context.SaveChangesAsync(ct);
        return comerciante;
    }

    public async Task UpdateAsync(Comerciante comerciante, CancellationToken ct = default)
    {
        _context.Comerciantes.Update(comerciante);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _context.Comerciantes.FindAsync(new object[] { id }, ct);
        if (entity != null)
        {
            _context.Comerciantes.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken ct = default) =>
        await _context.Comerciantes.AnyAsync(c => c.Id == id, ct);
}
