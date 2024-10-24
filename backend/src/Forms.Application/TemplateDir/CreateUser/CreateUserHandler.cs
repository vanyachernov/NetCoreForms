using CSharpFunctionalExtensions;
using Forms.Application.UserDir;
using Forms.Domain.Shared;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Domain.TemplateManagement.ValueObjects;

namespace Forms.Application.TemplateDir.CreateUser;

public class CreateUserHandler(IUsersRepository usersRepository)
{
    public async Task<Result<Guid, Error>> Handle(
        CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var fullName = FullName.Create(
            request.FullName.FirstName, 
            request.FullName.LastName).Value;
        
        var email = Email.Create(request.Email.Email).Value;
        
        var emailParts = email.Value.Split('@');
        
        var username = emailParts[0];
        
        var userToCreate = User.Create(
            fullName,
            email);
        
        if (userToCreate.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Creating user");
        }

        userToCreate.Value.UserName = username;
        
        await usersRepository.Register(
            userToCreate.Value, 
            request.Password.Password, 
            cancellationToken);

        Guid.TryParse(
            userToCreate.Value.Id, 
            out var userToCreateIdentifier);

        return userToCreateIdentifier;
    }
}