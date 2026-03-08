using ComercioApi.Core.Entities;
using ComercioApi.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ComercioApi.Infrastructure.Repositories;

public class CachedMunicipioRepository : IMunicipioRepository
{
    private readonly MunicipioRepository _inner;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "Municipios_All";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

    public CachedMunicipioRepository(MunicipioRepository inner, IMemoryCache cache)
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task<IReadOnlyList<Municipio>> GetAllAsync(CancellationToken ct = default)
    {
        var result = await _cache.GetOrCreateAsync(CacheKey, async entry =>
        {
            entry!.AbsoluteExpirationRelativeToNow = CacheDuration;
            return await _inner.GetAllAsync(ct);
        });
        return result ?? Array.Empty<Municipio>();
    }
}
