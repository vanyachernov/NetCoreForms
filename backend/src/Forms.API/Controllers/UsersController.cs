using CSharpFunctionalExtensions;
using Forms.Application.UserDir.GetUsers;
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
}