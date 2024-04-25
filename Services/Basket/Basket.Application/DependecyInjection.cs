using Basket.Application.GrpcServices;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Basket.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
                cfg.ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod!.IsAssembly;
                cfg.ShouldMapField = f => f.IsPublic || f.IsAssembly;
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<DiscountGrpcService>();
            return services;
        }
    }
}
