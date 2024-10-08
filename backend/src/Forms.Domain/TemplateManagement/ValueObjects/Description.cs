using CSharpFunctionalExtensions;
using Forms.Domain.Shared;

namespace Forms.Domain.TemplateManagement.ValueObjects;

public record Description
{
    private Description(string value) => Value = value;

    public string Value { get; } = default!;

    public static Result<Description> Create(string description)
    {
        return string.IsNullOrWhiteSpace(description) 
            ? Result.Failure<Description>("Description is invalid!") 
            : new Description(description);
    }
};