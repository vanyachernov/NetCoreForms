using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Forms.Infrastructure;

public class TemplateDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration _configuration;

    public TemplateDbContext(
        IConfiguration configuration, 
        DbContextOptions<TemplateDbContext> options)
        : base(options)
    {
        _configuration = configuration;
    }
    
    public DbSet<Template> Templates { get; set; }
    public DbSet<TemplateRoles> TemplateRoles { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        
        var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
        var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var dbUser = Environment.GetEnvironmentVariable("DB_USER");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
        
        var connectionString = $"Host={dbServer};" +
                               $"Port={dbPort};" +
                               $"Database={dbName};" +
                               $"Username={dbUser};" +
                               $"Password={dbPassword};" +
                               "SSL Mode=Require;" +
                               "Trust Server Certificate=true;";
        
        optionsBuilder
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TemplateDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
