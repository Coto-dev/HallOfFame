using HallOfFame.DAL.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace HallOfFame.BLL.Extensions;

public static class MigrateDbExtension {
    public static async Task MigrateDbAsync(this IApplicationBuilder builder) {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        // Migrate database
        var context = serviceScope.ServiceProvider.GetService<BackendDbContext>();
        if (context == null) {
            throw new ArgumentNullException(nameof(context));
        }
        await context.Database.MigrateAsync();
    }
}