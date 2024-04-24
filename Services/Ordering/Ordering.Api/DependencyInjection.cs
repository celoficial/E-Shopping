
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data;

namespace Ordering.Api
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
                    Title = "Ordering API",
                    Version = "v1"
                });
            });

            services.AddHealthChecks()
                .Services.AddDbContext<OrderContext>(o =>
                {
                    o.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
                });

            return services;
        }
    }
}
