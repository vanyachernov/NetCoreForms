using CSharpFunctionalExtensions;
using Forms.Domain.UserManagement.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Forms.Domain.UserManagement;

public class User : IdentityUser
{
    private User() { }

    private User(FullName fullName)
    {
        FullName = fullName;
    }

    public FullName FullName { get; private set; } = default!;

    public static Result<User> Create(FullName fullName)
    {
        return new User(fullName);
    }
}