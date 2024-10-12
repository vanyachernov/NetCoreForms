using Forms.API.Controllers.Shared;
using Forms.Application.UserDir.AuthenticateUser;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Infrastructure.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController(
    UserManager<User> userManager) : ApplicationController
{
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(
        [FromBody] AuthenticateUserRequest request,
        [FromServices] JwtHandler handler,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);

        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password!))
        {
            return Unauthorized(new AuthenticateUserResponse { ErrorMessage = "Invalid Authentication" });
        }

        var token = handler.CreateToken(user);

        return Ok(
            new AuthenticateUserResponse
            {
                IsAuthSuccessful = true,  
                Token = token
            }
        );
    }
}