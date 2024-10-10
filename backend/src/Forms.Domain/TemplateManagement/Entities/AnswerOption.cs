using CSharpFunctionalExtensions;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.ValueObjects;

namespace Forms.Domain.TemplateManagement.Entities;

public class AnswerOption : Shared.Entity<AnswerOptionId>
{
    private AnswerOption(AnswerOptionId id) : base(id) { }

    private AnswerOption(
        AnswerOptionId id,
        Question question,
        AnswerValue answerValue,
        IsCorrect isCorrect) : base(id)
    {
        Question = question;
        AnswerValue = answerValue;
        IsCorrect = isCorrect;
    }

    public Question Question { get; private set; } = default!;
    public AnswerValue AnswerValue { get; private set; } = default!;
    public IsCorrect IsCorrect { get; private set; } = default!;

    public static Result<AnswerOption> Create(
        AnswerOptionId id,
        Question question,
        AnswerValue answerValue,
        IsCorrect isCorrect)
    {
        return new AnswerOption(
            id, 
            question, 
            answerValue, 
            isCorrect);
    }
}