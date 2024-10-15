using CSharpFunctionalExtensions;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.Aggregate;

namespace Forms.Domain.TemplateManagement.Entities;

public class Instance : Shared.Entity<InstanceId>
{
    private readonly List<Answer> _answers;

    private Instance(InstanceId id) : base(id)
    {
        _answers = [];
    }

    private Instance(
        InstanceId id,
        Template template,
        User respondent,
        DateTime createdAt,
        DateTime finishedAt) : base(id)
    {
        Template = template;
        Respondent = respondent;
        CreatedAt = createdAt;
        _answers = [];
    }

    public Template Template { get; private set; } = default!;
    public User Respondent { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public DateTime FinishedAt { get; private set; }
    public IReadOnlyCollection<Answer> Answers => _answers;

    public void AddAnswer(Answer answer) => _answers.Add(answer);

    public static Result<Instance> Create(
        InstanceId id,
        Template template,
        User respondent,
        DateTime createdAt,
        DateTime finishedAt)
    {
        return new Instance(
            id, 
            template,
            respondent,
            createdAt,
            finishedAt);
    }
}