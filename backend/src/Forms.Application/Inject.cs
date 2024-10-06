using Forms.Application.IdentityManagement.Admin;
using Forms.Application.IdentityManagement.Roles;
using Microsoft.Extensions.DependencyInjection;

namespace Forms.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<EnsureAdminHandler>();
        
        services.AddScoped<EnsureRolesHandler>();
            
        return services;
    }
}