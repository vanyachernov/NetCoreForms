using CSharpFunctionalExtensions;

namespace Forms.Domain.TemplateManagement.ValueObjects;

public record AnswerValue
{
    private AnswerValue(string value)
    {
        Value = value;
    }

    public string Value { get; } = default!;

    public static Result<AnswerValue> Create(string answer)
    {
        return string.IsNullOrWhiteSpace(answer) 
            ? Result.Failure<AnswerValue>("Answer is invalid!") 
            : new AnswerValue(answer);
    }
};