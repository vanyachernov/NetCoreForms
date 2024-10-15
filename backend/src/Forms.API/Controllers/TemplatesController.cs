using Forms.API.Controllers.Shared;
using Forms.API.Extensions;
using Forms.API.Response;
using Forms.Application.TemplateDir.AddQuestion;
using Forms.Application.TemplateDir.Create;
using Forms.Application.TemplateDir.GetQuestions;
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
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateTemplateRequest request,
        [FromServices] CreateTemplateHandler template,
        CancellationToken cancellationToken = default)
    {
        var createTemplateResult = await template.Handle(
            request,
            cancellationToken);

        return Ok(createTemplateResult.Value);
    }
    
    [HttpPost("questions/{questionId:guid}")]
    public async Task<ActionResult<Guid>> AddQuestion(
        [FromRoute] Guid questionId,
        [FromBody] AddQuestionRequest request,
        [FromServices] AddQuestionHandler template,
        CancellationToken cancellationToken = default)
    {
        var createTemplateResult = await template.Handle(
            questionId,
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
        
        return Ok(Envelope.Ok(questionsTemplateResult.Value));
    }

}