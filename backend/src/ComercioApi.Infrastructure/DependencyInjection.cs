using ComercioApi.Application.Configuration;
using ComercioApi.Core.Interfaces;
using ComercioApi.Infrastructure.Data;
using ComercioApi.Infrastructure.Repositories;
using ComercioApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComercioApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddDbContext<ComercioDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IReporteComerciantesService, ReporteComerciantesService>();
        services.AddScoped<IComercianteRepository, ComercianteRepository>();
        services.AddScoped<MunicipioRepository>();
        services.AddScoped<IMunicipioRepository, CachedMunicipioRepository>();

        services.AddMemoryCache();

        return services;
    }
}
