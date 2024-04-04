using Core.Application.Abstractions.Persistence;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
            .AddTransient(typeof(IBaseReadRepository<>), typeof(BaseRepository<>))
            .AddTransient(typeof(IBaseWriteRepository<>), typeof(BaseRepository<>))
            .AddScoped<IDatabaseMigrator, DatabaseMigrator>();
    }
}