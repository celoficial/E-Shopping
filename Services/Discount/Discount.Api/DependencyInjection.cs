namespace Discount.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new()
                {
                    Title = "Discount API",
                    Version = "v1"
                });
            });

            services.AddGrpc();

            return services;
        }
    }
}
