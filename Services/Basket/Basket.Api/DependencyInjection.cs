using Discount.Grpc.Protos;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Basket.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddApiVersioning();

            services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString") ?? "";
            });

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new()
                {
                    Title = "Basket API",
                    Version = "v1"
                });
            });

            services.AddHealthChecks()
                .AddRedis(configuration.GetValue<string>("CacheSettings:ConnectionString") ?? "",
                    "Redis Health Check",
                    HealthStatus.Degraded);

            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
            {
                o.Address = new Uri(configuration.GetValue<string>("GrpcSettings:DiscountUrl") ?? "");
            });

            return services;
        }
    }
}
