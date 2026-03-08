using ComercioApi.Application.Interfaces;
using ComercioApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ComercioApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IComerciantesService, ComerciantesService>();
        services.AddScoped<IMunicipiosService, MunicipiosService>();
        return services;
    }
}
