using System.Reflection;
using Core.Application.Abstractions;
using Core.Application.Abstractions.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Api;

public static class WebApplicationExtensions
{
    public static WebApplication RunDbMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();

        var migrationAttemptsCount = 0;
        while (dataContext.GetPendingMigrations().Any())
        {
            migrationAttemptsCount++;
            try
            {
                if (dataContext.GetPendingMigrations().Any())
                {
                    dataContext.Migrate();
                }
            }
            catch (Exception ex)
            {

                if (migrationAttemptsCount == 10)
                {
                    throw;
                }

                app.Logger.LogWarning("{ex.Message}", ex);
                Thread.Sleep(2000);
            }
        }
        return app;
    }

    public static WebApplication RegisterApis(this WebApplication app, Assembly assembly, string baseApiRef)
    {
        var apiTypeInterface = typeof(IApi);
        var apiTypes = assembly.GetTypes()
            .Where(p => apiTypeInterface.IsAssignableFrom(p) && p.IsClass);

        foreach (var apiType in apiTypes)
        {
            var api = (IApi)Activator.CreateInstance(apiType)!;
            api.Register(app, $"{baseApiRef}");
        }

        return app;
    }
}