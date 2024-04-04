using Core.Application.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreApplicationServices(this IServiceCollection services)
    {
        return services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>));
    }
}