using CSharpFunctionalExtensions;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.Aggregate;

namespace Forms.Domain.TemplateManagement.Entities;

public class Instance : Shared.Entity<InstanceId>
{
    private readonly List<Answer> _answers = [];
    
    private Instance(InstanceId id) : base(id) { }

    private Instance(
        InstanceId id,
        User respondent,
        Template template) : base(id)
    {
        Respondent = respondent;
        Template = template;
    }

    public User Respondent { get; private set; } = default!;
    public Template Template { get; private set; } = default!;
    public IReadOnlyCollection<Answer> Answers => _answers;

    public static Result<Instance> Create(
        InstanceId id,
        User respondent,
        Template template)
    {
        return new Instance(id, respondent, template);
    }
}