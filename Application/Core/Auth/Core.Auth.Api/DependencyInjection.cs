using System.Reflection;
using System.Text;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Core.Auth.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreAuthApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });

        var authBuilder = services.AddAuthorizationBuilder();
        authBuilder
            .AddPolicy(AuthorizationPoliciesEnum.AdminGreetings.ToString(), policy =>
                policy.RequireRole(ApplicationUserRolesEnum.Admin.ToString()));
        
        return services.AddTransient<ICurrentUserService, CurrentUserService>();
    }

    public static IServiceCollection AddSwaggerWidthJwtAuth(
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

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = """
                                  JWT Authorization header using the Bearer scheme. \r\n\r\n
                                                        Enter 'Bearer' [space] and then your token in the text input below.
                                                        \r\n\r\nExample: 'Bearer 12345abcdef'
                                  """,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{apiAssembly.GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
    }
}