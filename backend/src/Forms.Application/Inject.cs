using Forms.Application.Identity.Admin;
using Forms.Application.Identity.Roles;
using Forms.Application.Template.AddQuestion;
using Forms.Application.Template.GetUsers;
using Microsoft.Extensions.DependencyInjection;

namespace Forms.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<EnsureAdminHandler>();
        
        services.AddScoped<EnsureRolesHandler>();

        services.AddScoped<AddQuestionHandler>();
        
        services.AddScoped<GetUsersHandler>();
            
        return services;
    }
}