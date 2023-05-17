using Microsoft.OpenApi.Models;

namespace AAS_BSL.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection SwaggerSetup(this IServiceCollection service)
    {
        return service.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v2",
                Title = "AAS_Deliveroo",
                Description = "Web API for AAS Deliveroo",
            });
        });
    }

    public static IApplicationBuilder SwaggerAppSetup(this IApplicationBuilder builder)
    {
        return builder.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
        }).UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }
}