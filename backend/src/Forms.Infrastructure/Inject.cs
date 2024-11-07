using Forms.Application.JiraDir;
using Forms.Application.Providers;
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

        services.AddScoped<ITokensRepository, TokensRepository>();
        
        services.AddScoped<IJiraService, JiraService>();
        
        services.AddHttpClient<IJiraService, JiraService>();
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        services.AddScoped<JwtHandler>();
        
        return services;
    }
}