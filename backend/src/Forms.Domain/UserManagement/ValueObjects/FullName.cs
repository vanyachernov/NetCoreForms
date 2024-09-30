using CSharpFunctionalExtensions;
using Forms.Domain.Shared;

namespace Forms.Domain.UserManagement.ValueObjects;

public record FullName
{
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;

    public static Result<FullName> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) && firstName.Length > Constants.MAX_FULL_NAME_TEXT_LENGTH)
        {
            return Result.Failure<FullName>("FirstName is invalid!");
        }
        
        if (string.IsNullOrWhiteSpace(lastName) && lastName.Length > Constants.MAX_FULL_NAME_TEXT_LENGTH)
        {
            return Result.Failure<FullName>("LastName is invalid!");
        }

        return new FullName(firstName, lastName);
    }
};