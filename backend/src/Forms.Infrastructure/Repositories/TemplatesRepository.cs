using CSharpFunctionalExtensions;
using Forms.Application.TemplateDir;
using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.Entities;

namespace Forms.Infrastructure.Repositories;

public class TemplatesRepository : ITemplatesRepository
{
    private readonly TemplateDbContext _templateContext;

    public TemplatesRepository(TemplateDbContext context)
    {
        _templateContext = context;
    }
    
    public async Task<Guid> Create(
        Template template, 
        CancellationToken cancellationToken = default)
    {
        await _templateContext.Templates
            .AddAsync(
                template, 
                cancellationToken);

        await _templateContext.SaveChangesAsync(cancellationToken);
        
        return template.Id.Value;
    }

    public Task<Guid> AddQuestion(
        Question question, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}