using CSharpFunctionalExtensions;
using Forms.Domain.Shared.IDs;
using Forms.Domain.Shared.ValueObjects;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Domain.TemplateManagement.ValueObjects;

namespace Forms.Domain.TemplateManagement.Aggregate;

public sealed class Template : Shared.Entity<TemplateId>
{
    private readonly List<Question> _questions = [];
    
    private Template(TemplateId id) : base(id) { }

    private Template(
        TemplateId id,
        Title title,
        Description description) : base(id)
    {
        Title = title;
        Description = description;
    }

    public Title Title { get; private set; } = default!;
    public Description Description { get; private set; } = default!;

    public IReadOnlyCollection<Question> Questions => _questions;

    public static Result<Template> Create(
        TemplateId id, 
        Title title, 
        Description description)
    {
        return new Template(id, title, description);
    }
}