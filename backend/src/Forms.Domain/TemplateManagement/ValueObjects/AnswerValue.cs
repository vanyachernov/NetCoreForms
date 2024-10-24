using CSharpFunctionalExtensions;
using Forms.Domain.Shared;

namespace Forms.Domain.TemplateManagement.ValueObjects;

public record AnswerValue
{
    private AnswerValue(string value) => Value = value;

    public string? Value { get; }

    public static Result<AnswerValue, Error> Create(string answerValue)
    {
        return string.IsNullOrWhiteSpace(answerValue)
            ? Errors.General.ValueIsInvalid("Answer")
            : new AnswerValue(answerValue);
    }
};