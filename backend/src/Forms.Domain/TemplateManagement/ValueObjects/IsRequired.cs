using CSharpFunctionalExtensions;

namespace Forms.Domain.TemplateManagement.ValueObjects;

public record IsRequired
{
    private IsRequired(bool value)
    {
        Value = value;
    }
    
    public bool Value { get; }

    public static Result<IsRequired> Create(bool isRequired)
    {
        return new IsRequired(isRequired);
    }
};