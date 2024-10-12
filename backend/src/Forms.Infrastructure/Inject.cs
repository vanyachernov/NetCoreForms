using Forms.Application.TemplateDir;
using Forms.Application.UserDir;
using Forms.Infrastructure.Providers;
using Forms.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Forms.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<TemplateDbContext>();
        
        services.AddScoped<ITemplatesRepository, TemplatesRepository>();
        
        services.AddScoped<IUsersRepository, UsersRepository>();
        
        services.AddScoped<JwtHandler>();
        
        return services;
    }
}