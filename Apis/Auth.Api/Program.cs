using System.Reflection;
using Auth.Application;
using Serilog;
using Serilog.Events;
using Core.Api;
using Core.Api.Middlewares;
using Core.Application;
using Core.Auth.Api;
using Core.Auth.Application;
using Core.Auth.Application.Middlewares;
using Infrastructure.Persistence;

try
{
    const string appPrefix = "auth";
    const string version = "v1";
    const string appName = "Auth API v1";
    
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
#if DEBUG
        .WriteTo.Console()
#endif
        .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Information-.txt", LogEventLevel.Information,
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3, buffered: true)
        .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Warning-.txt", LogEventLevel.Warning,
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14, buffered: true)
        .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Error-.txt", LogEventLevel.Error,
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30, buffered: true));
    
    builder.Services
        .AddSwaggerWidthJwtAuth(Assembly.GetExecutingAssembly(), appName, version, appName )
        .AddCoreApiServices()
        .AddCoreApplicationServices()
        .AddCoreAuthApiServices(builder.Configuration)
        .AddPersistenceServices(builder.Configuration)
        .AddCoreAuthServices()
        .AddAllCors()
        .AddAuthApplication();

    var app = builder.Build();
    
    app.RunDbMigrations().RegisterApis(Assembly.GetExecutingAssembly(), $"{appPrefix}/api/{version}");
    
    app.UseCoreExceptionHandler()
        .UseAuthExceptionHandler()
        .UseSwagger(c => { c.RouteTemplate = appPrefix + "/swagger/{documentname}/swagger.json"; })
        .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/" + appPrefix + $"/swagger/{version}/swagger.json", version);
                options.RoutePrefix = appPrefix + "/swagger";
            })
        .UseAuthentication()
        .UseAuthorization()
        .UseHttpsRedirection();
    
    app.Run();
}
catch (Exception ex)
{
    var appSettingsFile = $"{Directory.GetCurrentDirectory()}/appsettings.json";
    var settingsJson = File.ReadAllText(appSettingsFile);
    var appSettings = System.Text.Json.JsonDocument.Parse(settingsJson);
    var logsPath = appSettings.RootElement.GetProperty("Logging").GetProperty("LogsFolder").GetString();
    var logger = new LoggerConfiguration()
        .WriteTo.File($"{logsPath}/Log-Run-Error-.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Hour,
            retainedFileCountLimit: 30)
        .CreateLogger();
    logger.Fatal(ex.Message, ex);
}