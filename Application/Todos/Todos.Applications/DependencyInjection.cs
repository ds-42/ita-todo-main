using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Todos.Applications.Caches;

namespace Todos.Applications;

public static class DependencyInjection
{
    public static IServiceCollection AddTodosApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddSingleton<TodoMemoryCache>()
            .AddSingleton<TodosListMemoryCache>()
            .AddSingleton<TodosCountMemoryCache>()
            .AddTransient<ICleanTodosCacheService, CleanTodosCacheService>();
    }
}