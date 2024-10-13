using CSharpFunctionalExtensions;
using Forms.Domain.Shared;

namespace Forms.Domain.TemplateManagement.ValueObjects;

public record RefreshToken
{
    private RefreshToken(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<RefreshToken> Create(string token)
    {
        return new RefreshToken(token);
    }
};