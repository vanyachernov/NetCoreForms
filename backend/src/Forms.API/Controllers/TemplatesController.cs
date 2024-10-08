using Forms.Application.TemplateDir.Create;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TemplatesController : ControllerBase
{
    [HttpPost]
    public async Task<Guid> Create(
        [FromBody] CreateTemplateRequest request,
        [FromServices] CreateTemplateHandler template,
        CancellationToken cancellationToken = default)
    {
        var createTemplateResult = await template.Handle(
            request,
            cancellationToken);

        return createTemplateResult.Value;
    }
}