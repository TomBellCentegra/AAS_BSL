using AAS_BSL.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.SwaggerSetup();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Logging.AddAzureWebAppDiagnostics();

StartupExtensions.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.SwaggerAppSetup();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();