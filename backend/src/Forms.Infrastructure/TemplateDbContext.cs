using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Forms.Infrastructure;

public class TemplateDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration _configuration;
    private const string DATABASE = nameof(Database);

    public TemplateDbContext(IConfiguration configuration, DbContextOptions<TemplateDbContext> options)
        : base(options)
    {
        _configuration = configuration;
    }
    
    public DbSet<Template> Templates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseNpgsql(_configuration.GetConnectionString(DATABASE))
                .UseSnakeCaseNamingConvention();
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TemplateDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
