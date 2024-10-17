using Forms.API.Controllers.Shared;
using Forms.API.Extensions;
using Forms.API.Response;
using Forms.Application.TemplateDir.AddQuestion;
using Forms.Application.TemplateDir.AddUserAccessToTemplate;
using Forms.Application.TemplateDir.Create;
using Forms.Application.TemplateDir.GetQuestions;
using Forms.Application.TemplateDir.GetTemplate;
using Forms.Application.TemplateDir.GetTemplates;
using Forms.Domain.Shared;
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

        return Ok(templatesResult.Value);
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
    
    /// <summary>
    /// Create new template instance.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="request">Request.</param>
    /// <param name="templateHandler">Template handler.</param>
    /// <param name="accessHandler">Access handler.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    [HttpPost("{userId:guid}")]
    public async Task<ActionResult<Guid>> Create(
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

        return createTemplateResult.IsFailure 
            ? createTemplateResult.Error.ToResponse() 
            : Ok(createTemplateResult.Value);
    }
    
    /// <summary>
    /// Add question to template.
    /// </summary>
    /// <param name="request">Request.</param>
    /// <param name="template">Template handler.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A new question.</returns>
    [HttpPost("questions")]
    public async Task<ActionResult<Guid>> AddQuestion(
        [FromBody] AddQuestionRequest request,
        [FromServices] AddQuestionHandler template,
        CancellationToken cancellationToken = default)
    {
        var createTemplateResult = await template.Handle(
            request,
            cancellationToken);

        return Ok(createTemplateResult.Value);
    }
    
    /// <summary>
    /// Get all template's questions.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="questionsHandler">Question handler.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Questions.</returns>
    [HttpGet("{templateId:guid}/questions")]
    public async Task<ActionResult> GetQuestions(
        [FromRoute] Guid templateId,
        [FromServices] GetQuestionsHandler questionsHandler,
        CancellationToken cancellationToken = default)
    {
        var questionsTemplateResult = await questionsHandler.Handle(
            templateId, 
            cancellationToken);
        
        return questionsTemplateResult.IsFailure 
            ? questionsTemplateResult.Error.ToResponse() 
            : Ok(questionsTemplateResult.Value);
    }
}