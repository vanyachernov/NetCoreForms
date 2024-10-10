using Forms.Application.TemplateDir.Create;
using Forms.Application.TemplateDir.GetQuestions;
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
    
    [HttpPost("questions")]
    public async Task<Guid> AddQuestion(
        [FromBody] CreateTemplateRequest request,
        [FromServices] CreateTemplateHandler template,
        CancellationToken cancellationToken = default)
    {
        var createTemplateResult = await template.Handle(
            request,
            cancellationToken);

        return createTemplateResult.Value;
    }
    
    [HttpGet("{templateId:guid}/questions")]
    public async Task<IEnumerable<GetQuestionsResponse>> GetQuestions(
        [FromRoute] Guid templateId,
        [FromServices] GetQuestionsHandler questions,
        CancellationToken cancellationToken = default)
    {
        var questionsTemplateResult = await questions.Handle(
            templateId,
            cancellationToken);

        return questionsTemplateResult;
    }
}