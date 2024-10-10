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
        [FromBody] CreateTemplateRequest request,
        [FromServices] CreateTemplateHandler template,
        CancellationToken cancellationToken = default)
    {
        var createTemplateResult = await template.Handle(
            request,
            cancellationToken);

        return Ok(createTemplateResult.Value);
    }
    
    [HttpGet("{templateId:guid}/questions")]
    public async Task<ActionResult<IEnumerable<GetQuestionsResponse>>> GetQuestions(
        [FromRoute] Guid templateId,
        [FromServices] GetQuestionsHandler questions,
        CancellationToken cancellationToken = default)
    {
        var questionsTemplateResult = await questions.Handle(
            templateId,
            cancellationToken);

        return Ok(questionsTemplateResult.Value);
    }
}