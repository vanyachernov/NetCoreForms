using CSharpFunctionalExtensions;
using Forms.API.Controllers.Shared;
using Forms.API.Extensions;
using Forms.Application.DTOs;
using Forms.Application.TemplateDir.CreateUser;
using Forms.Application.UserDir;
using Forms.Application.UserDir.AuthenticateUser;
using Forms.Domain.Shared;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ApplicationController
{
    private readonly UserManager<User> _usersManager;
    private readonly ITokensRepository _tokensRepository;

    public AccountsController(
        UserManager<User> usersManager,
        ITokensRepository tokensRepository)
    {
        _usersManager = usersManager;
        _tokensRepository = tokensRepository;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] CreateUserRequest request,
        [FromServices] CreateUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var user = await _usersManager.FindByEmailAsync(request.Email.Email!);

        if (user is not null)
        {
            var conflictError = Error.NotFound(
                "user.exists", 
                "User with this email already exists");
            return conflictError.ToResponse();
        }

        var result = await handler.Handle(
            request, 
            cancellationToken);
        
        return result.IsFailure 
            ? result.Error.ToResponse() 
            : Ok(result.Value);
    }
    
    [HttpPost("authenticate")]
    public async Task<ActionResult<Result<Error>>> Authenticate(
        [FromBody] AuthenticateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _usersManager.FindByEmailAsync(request.Email!);

        if (user is null || !await _usersManager.CheckPasswordAsync(
                user, 
                request.Password!))
        {
            return Unauthorized(new AuthenticateUserResponse
            {
                ErrorMessage = "Invalid Authentication"
            });
        }

        var tokenResult = await _tokensRepository.CreateToken(
            user, 
            populateExp: true, 
            cancellationToken);

        if (tokenResult.IsFailure)
        {
            Errors.General.ValueIsInvalid("Token");
        }

        return Ok(tokenResult.Value);
    }

    [HttpPost("refresh/{userId:guid}")]
    public async Task<ActionResult<Result<Error>>> Refresh(
        [FromRoute] Guid userId,
        [FromBody] TokenDto tokenDto,
        CancellationToken cancellationToken = default)
    {
        var user = await _usersManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return Unauthorized(new AuthenticateUserResponse
            {
                ErrorMessage = "Invalid User"
            });
        }
        
        var tokenDtoToReturn = await _tokensRepository.RefreshToken(
            user,
            tokenDto, 
            cancellationToken);

        if (tokenDtoToReturn.IsFailure)
        {
            Errors.General.ValueIsInvalid("RefreshToken");
        }

        return Ok(tokenDtoToReturn.Value);
    }
}