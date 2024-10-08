using Microsoft.AspNetCore.Identity;

namespace Forms.Application.Identity.Roles;

public class EnsureRolesHandler(RoleManager<IdentityRole> roleManager)
{
    public async Task Handle()
    {
        var roles = new[] { "Admin", "Creator", "Reader" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}