using Forms.Domain.TemplateManagement.Entities;
using Forms.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Forms.API;

public static class Inject
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        
        services.AddControllers();

        services.AddDbContext<TemplateDbContext>();

        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = string.Empty;
            })
            .AddEntityFrameworkStores<TemplateDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}