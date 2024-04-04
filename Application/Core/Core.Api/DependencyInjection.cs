using System.Reflection;
using System.Text.Json.Serialization;
using Core.Api.JsonSerializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;

namespace Core.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreApiServices(this IServiceCollection services)
    {
        return services
            .AddHttpContextAccessor()
            .Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new TrimmingConverter());
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            })
            .AddResponseCompression(options => { options.EnableForHttps = true; });
    }

    public static IServiceCollection AddAllCors(this IServiceCollection services)
    {
        return services
            .AddCors(options =>
            {
                options.AddPolicy(CorsPolicy.AllowAll, policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                    policy.WithExposedHeaders("*");
                });
            });
    }
    
    public static IServiceCollection AddSwagger(
        this IServiceCollection services,
        Assembly apiAssembly,
        string appName,
        string version,
        string description)
    {
        return services.AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = appName,
                    Description = description
                });
                
                // using System.Reflection;
                var xmlFilename = $"{apiAssembly.GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
    }
}