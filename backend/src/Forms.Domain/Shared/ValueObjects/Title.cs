using CSharpFunctionalExtensions;

namespace Forms.Domain.Shared.ValueObjects;

public record Title
{
    private Title(string value)
    {
        Value = value;
    }

    public string Value { get; } = default!;

    public static Result<Title> Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Length > Constants.MAX_TITLE_TEXT_LENGTH)
        {
            return Result.Failure<Title>("Title is invalid!");
        }

        return new Title(title);
    }
};