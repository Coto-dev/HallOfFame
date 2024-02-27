using HallOfFame.BLL.Services;
using HallOfFame.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HallOfFame.BLL.Extensions;

public static class ServiceDependencyExtension {
    /// <summary>
    /// Add BLL services dependency
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBllServices(this IServiceCollection services) {
        services.AddScoped<IPersonService, PersonService>();

        return services;
    }
}