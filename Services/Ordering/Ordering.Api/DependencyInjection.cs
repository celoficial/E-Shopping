
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Ordering.Api.EventBusConsumer;
using Ordering.Infrastructure.Data;

namespace Ordering.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddApiVersioning();

            services.AddScoped<BasketOrderingConsumer>();

            services.AddMassTransit(o =>
            {
                o.AddConsumer<BasketOrderingConsumer>();
                o.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
                    {
                        c.ConfigureConsumer<BasketOrderingConsumer>(context);
                    });

                    cfg.Host(configuration["EventBusSettings:Server"], "/", h =>
                    {
                        h.Username(configuration["EventBusSettings:UserName"]);
                        h.Password(configuration["EventBusSettings:Password"]);
                    });
                });
            });

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
