using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}
