using Forms.API.Controllers.Shared;
using Forms.API.Extensions;
using Forms.Application.TemplateDir.GetTemplate;
using Forms.Application.TemplateDir.GetTemplates;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TemplatesController : ApplicationController
{
    /// <summary>
    /// Get templates list.
    /// </summary>
    /// <param name="template">Template handler.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Templates list.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTemplatesResponse>>> Get(
        [FromServices] GetTemplatesHandler template,
        CancellationToken cancellationToken = default)
    {
        var templatesResult = await template.Handle(cancellationToken);

        return templatesResult.IsFailure 
            ? templatesResult.Error.ToResponse() 
            : Ok(templatesResult.Value);
    }

    /// <summary>
    /// Get specify template by identifier.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="template">Template handler.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Template.</returns>
    [HttpGet("{templateId:guid}")]
    public async Task<ActionResult<GetTemplatesResponse>> GetTemplateById(
        [FromRoute] Guid templateId,
        [FromServices] GetTemplateHandler template,
        CancellationToken cancellationToken = default)
    {
        var templateResult = await template.Handle(
            templateId,
            cancellationToken);

        return templateResult.IsFailure 
            ? templateResult.Error.ToResponse() 
            : Ok(templateResult.Value);
    }
}