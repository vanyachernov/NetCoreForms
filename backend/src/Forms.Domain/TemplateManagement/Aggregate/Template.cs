using CSharpFunctionalExtensions;
using Forms.Domain.Shared.IDs;
using Forms.Domain.Shared.ValueObjects;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Domain.TemplateManagement.ValueObjects;

namespace Forms.Domain.TemplateManagement.Aggregate;

public sealed class Template : Shared.Entity<TemplateId>
{
    private readonly List<Question> _questions;
    private readonly List<TemplateRoles> _roles;

    private Template(TemplateId id) : base(id)
    {
        _questions = [];
    }

    private Template(
        TemplateId id,
        User owner,
        Title title,
        Description description) : base(id)
    {
        Owner = owner;
        Title = title;
        Description = description;
        _questions = [];
    }

    public User Owner { get; private set; } = default!;
    public Title Title { get; private set; } = default!;
    public Description Description { get; private set; } = default!;

    public IReadOnlyCollection<Question> Questions => _questions;
    public IReadOnlyCollection<TemplateRoles> Roles => _roles;

    public void AddQuestion(Question question) => _questions.Add(question);

    public static Result<Template> Create(
        TemplateId id, 
        User owner,
        Title title, 
        Description description)
    {
        return new Template(
            id, 
            owner, 
            title, 
            description);
    }
}