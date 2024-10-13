using System.Text;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Forms.API;

public static class Inject
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        DotNetEnv.Env.Load();

        services.AddEndpointsApiExplorer();
        
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
        
        var jwtSecurityKey = Environment.GetEnvironmentVariable("JWT_SECRET");
        var jwtValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        var jwtValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtOptions =>
        {
            jwtOptions.UseSecurityTokenValidators = true;

            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtValidIssuer,
                ValidAudience = jwtValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey!))
            };
        });
        
        return services;
    }
}