using Forms.API.Controllers.Shared;
using Forms.API.Extensions;
using Forms.API.Response;
using Forms.Application.TemplateDir.AddQuestion;
using Forms.Application.TemplateDir.AddUserAccessToTemplate;
using Forms.Application.TemplateDir.Create;
using Forms.Application.TemplateDir.GetQuestions;
using Forms.Application.TemplateDir.GetTemplate;
using Forms.Application.TemplateDir.GetTemplates;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TemplatesController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTemplatesResponse>>> Get(
        [FromServices] GetTemplatesHandler template,
        CancellationToken cancellationToken = default)
    {
        var templatesResult = await template.Handle(cancellationToken);

        return Ok(templatesResult.Value);
    }

    [HttpGet("{templateId:guid}")]
    public async Task<ActionResult<GetTemplatesResponse>> GetTemplateById(
        [FromRoute] Guid templateId,
        [FromServices] GetTemplateHandler template,
        CancellationToken cancellationToken = default)
    {
        var templateResult = await template.Handle(
            templateId,
            cancellationToken);

        return Ok(templateResult.Value);
    }
    
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

        return Ok(createTemplateResult.Value);
    }
    
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
    
    [HttpGet("{templateId:guid}/questions")]
    public async Task<ActionResult> GetQuestions(
        [FromRoute] Guid templateId,
        [FromServices] GetQuestionsHandler questionsHandler,
        CancellationToken cancellationToken = default)
    {
        var questionsTemplateResult = await questionsHandler.Handle(
            templateId, 
            cancellationToken);
        
        if (questionsTemplateResult.IsFailure)
        {
            return questionsTemplateResult.Error
                .ToResponse();
        }
        
        return Ok(questionsTemplateResult.Value);
    }

}