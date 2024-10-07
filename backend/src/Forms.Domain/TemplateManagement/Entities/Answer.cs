using CSharpFunctionalExtensions;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.ValueObjects;

namespace Forms.Domain.TemplateManagement.Entities;

public class Answer : Shared.Entity<AnswerId>
{
    private Answer(AnswerId id) : base(id) { }

    private Answer(
        AnswerId id,
        Instance instance,
        Question question,
        AnswerValue answer) : base(id)
    {
        Question = question;
        Instance = instance;
        AnswerValue = answer;
    }
    
    public Question Question { get; private set; } = default!;
    public Instance Instance { get; private set; } = default!;
    public AnswerValue AnswerValue { get; private set; } = default!;

    public static Result<Answer> Create(
        AnswerId id,
        Instance instance,
        Question question,
        AnswerValue answer)
    {
        return new Answer(
            id, 
            instance,
            question, 
            answer);
    }
}
