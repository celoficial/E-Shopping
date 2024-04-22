using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Catalog.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddApiVersioning();

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new()
                {
                    Title = "Catalog API",
                    Version = "v1"
                });
            });

            services.AddHealthChecks()
                .AddMongoDb(configuration.GetValue<string>("DatabaseSettings:ConnectionString") ?? "",
                "Catalog Mongo Db Health Check",
                HealthStatus.Degraded);


            return services;
        }
    }
}
