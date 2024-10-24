using CSharpFunctionalExtensions;

namespace Forms.Domain.TemplateManagement.ValueObjects;

public record IsCorrect
{
    private IsCorrect(bool value) => Value = value;
    
    public bool Value { get; }

    public static Result<IsCorrect> Create(bool isCorrect)
    {
        return new IsCorrect(isCorrect);
    }
};