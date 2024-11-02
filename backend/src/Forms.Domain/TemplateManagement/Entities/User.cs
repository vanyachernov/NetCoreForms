using CSharpFunctionalExtensions;
using Forms.Domain.TemplateManagement.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Forms.Domain.TemplateManagement.Entities;

public class User : IdentityUser
{
    private User() { }

    private User(FullName fullName)
    {
        FullName = fullName;
    }

    public FullName FullName { get; private set; } = default!;
    public RefreshToken? RefreshToken { get; private set; }
    public RefreshTokenExpiryTime? RefreshTokenExpiryTime { get; private set; }
    
    public void SetEmail(Email email) => 
        Email = email.Value;
    public void SetUsername(string username) => 
        UserName = username;
    public void SetRefreshToken(RefreshToken token) => 
        RefreshToken = token;
    public void SetRefreshTokenExpiryTime(RefreshTokenExpiryTime tokenTime) => 
        RefreshTokenExpiryTime = tokenTime; 

    public static Result<User> Create(
        FullName fullName,
        Email email)
    {
        var user = new User(fullName);
        
        user.SetEmail(email);

        return user;
    }
}