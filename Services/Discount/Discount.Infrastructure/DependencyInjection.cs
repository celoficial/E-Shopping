using Discount.Core.Repositories;
using Discount.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            return services;
        }
    }
}
