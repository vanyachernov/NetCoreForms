using Microsoft.Extensions.DependencyInjection;

namespace Forms.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<TemplateDbContext>();
        
        return services;
    }
}