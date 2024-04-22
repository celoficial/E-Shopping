using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Discount.Application
{
    public static class DependencyInjection
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
            return services;
        }
    }
}
