using AAS_BSL.Infrastructure.TypeSearcher;
using AAS_BSL.Services.Company;
using AAS_BSL.Services.Secret;
using AAS_BSL.Infrastructure.Database;
using AAS_BSL.Services.HttpClient;
using AAS_BSL.Services.Subsription;

namespace AAS_BSL.Extensions;

public static class StartupExtensions
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var typeSearcher = new TypeSearcher();
        services.AddSingleton<ITypeSearcher>(typeSearcher);
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ISecretService, SecretService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        services.AddHttpClient<IBslHttpClient, BslHttpClient>();

        services.AddSingleton<CentegraProcessingDbContext>();
    }
}