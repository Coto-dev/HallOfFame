using HallOfFame.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HallOfFame.BLL.Extensions;

public static class ConfigureDbDependencies {
    /// <summary>
    /// Add database context dependency
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
       return services.AddDbContext<BackendDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("BackendDatabase")));
    }
}