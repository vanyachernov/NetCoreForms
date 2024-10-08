using Forms.Application.Identity.Admin;
using Forms.Application.Identity.Roles;
using Forms.Application.TemplateDir.AddQuestion;
using Forms.Application.TemplateDir.Create;
using Forms.Application.UserDir.GetUsers;
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
        
        services.AddScoped<CreateTemplateHandler>();
            
        return services;
    }
}