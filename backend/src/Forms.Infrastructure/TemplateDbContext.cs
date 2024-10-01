using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Forms.Infrastructure;

public class TemplateDbContext(IConfiguration configuration) : DbContext
{
    private const string DATABASE = nameof(Database);
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(configuration.GetConnectionString(DATABASE))
            .UseSnakeCaseNamingConvention();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TemplateDbContext).Assembly);
    }
}