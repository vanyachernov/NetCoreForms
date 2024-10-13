using CSharpFunctionalExtensions;
using Forms.Domain.Shared;

namespace Forms.Domain.TemplateManagement.ValueObjects;

public record RefreshTokenExpiryTime
{
    private RefreshTokenExpiryTime(DateTime value)
    {
        Value = value == default 
            ? DateTime.UtcNow 
            : value;
    }
    
    public DateTime Value { get; }

    public static Result<RefreshTokenExpiryTime> Create(DateTime? expiryTime)
    {
        return new RefreshTokenExpiryTime(expiryTime ?? DateTime.UtcNow);
    }
};