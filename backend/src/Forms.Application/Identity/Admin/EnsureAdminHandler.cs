using DotNetEnv;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Domain.TemplateManagement.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Forms.Application.Identity.Admin;

public class EnsureAdminHandler(UserManager<User> userManager)
{
    public async Task Handle()
    {
        Env.Load();

        var username = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
        var firstName = Environment.GetEnvironmentVariable("ADMIN_FIRST_NAME");
        var lastName = Environment.GetEnvironmentVariable("ADMIN_LAST_NAME");
        var email = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        var password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

        if (username != null && 
            email != null && 
            password != null && 
            firstName != null && 
            lastName != null && 
            await userManager.FindByEmailAsync(email) == null)
        {
            var userFullName = FullName.Create(firstName, lastName);
            var userEmail = Email.Create(email);
            var user = User.Create(userFullName.Value);

            user.Value.SetEmail(userEmail.Value);
            user.Value.SetUsername(username);

            var createUserResult = await userManager.CreateAsync(user.Value, password);
            
            if (!createUserResult.Succeeded)
            {
                throw new Exception($"Failed to create user {email}. Errors: {createUserResult.Errors}");
            }

            await userManager.AddToRoleAsync(user.Value, "Admin");
        }
    }
}