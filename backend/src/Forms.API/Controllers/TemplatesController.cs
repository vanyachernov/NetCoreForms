using Forms.API.Controllers.Shared;
using Forms.API.Extensions;
using Forms.API.Response;
using Forms.Application.TemplateDir.AddQuestion;
using Forms.Application.TemplateDir.Create;
using Forms.Application.TemplateDir.GetQuestions;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TemplatesController : ApplicationController
{
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
        
        return Ok(Envelope.Ok(questionsTemplateResult.Value));
    }

}