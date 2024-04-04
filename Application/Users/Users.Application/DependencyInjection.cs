using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Caches;

namespace Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUserApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddSingleton<ApplicationUsersListMemoryCache>()
            .AddSingleton<ApplicationUserMemoryCache>()
            .AddSingleton<ApplicationUsersCountMemoryCache>();
    }
}