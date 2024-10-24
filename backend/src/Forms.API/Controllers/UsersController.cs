using Forms.API.Controllers.Shared;
using Forms.API.Extensions;
using Forms.Application.TemplateDir.AddUserAccessToTemplate;
using Forms.Application.TemplateDir.CreateTemplate;
using Forms.Application.TemplateDir.GetTemplates;
using Forms.Application.TemplateDir.GetUserTemplates;
using Forms.Application.UserDir.GetUsers;
using Forms.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetUsersResponse>>> Get(
        [FromServices] GetUsersHandler users,
        CancellationToken cancellationToken = default)
    {
        var getUsersResult = await users.Handle(cancellationToken);

        if (getUsersResult.IsFailure)
        {
            return BadRequest([]);
        }

        return Ok(getUsersResult.Value);
    }
    
    /// <summary>
    /// Get templates for a specific user.
    /// </summary>
    [HttpGet("{userId:guid}/templates")]
    public async Task<ActionResult<IEnumerable<GetTemplatesResponse>>> GetUserTemplates(
        [FromRoute] Guid userId,
        [FromServices] GetUserTemplatesHandler template,
        CancellationToken cancellationToken = default)
    {
        var templatesResult = await template.Handle(userId, cancellationToken);

        return templatesResult.IsFailure 
            ? templatesResult.Error.ToResponse() 
            : Ok(templatesResult.Value);
    }
    
    /// <summary>
    /// Create a new template for a specific user.
    /// </summary>
    [HttpPost("{userId:guid}/templates")]
    public async Task<ActionResult<Guid>> CreateTemplate(
        [FromRoute] Guid userId,
        [FromBody] CreateTemplateRequest request,
        [FromServices] CreateTemplateHandler templateHandler,
        [FromServices] AddUserAccessToTemplateHandler accessHandler,
        CancellationToken cancellationToken = default)
    {
        var createTemplateResult = await templateHandler.Handle(
            userId, 
            request, 
            cancellationToken);

        if (createTemplateResult.IsFailure)
        {
            createTemplateResult.Error.ToResponse();
        }

        var roleResult = await accessHandler.Handle(
            userId,
            createTemplateResult.Value,
            TemplateRole.Owner,
            cancellationToken);

        return roleResult.IsFailure 
            ? roleResult.Error.ToResponse()
            : Ok(createTemplateResult.Value);
    }
}