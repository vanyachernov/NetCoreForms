using Forms.API.Controllers.Shared;
using Forms.API.Extensions;
using Forms.Application.TemplateDir.AddQuestion;
using Forms.Application.TemplateDir.GetQuestions;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers;

public class QuestionsController : ApplicationController
{
    /// <summary>
    /// Add question to template.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="request">Request.</param>
    /// <param name="template">Template handler.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A new question.</returns>
    [HttpPost("{templateId:guid}")]
    public async Task<ActionResult<Guid>> Add(
        [FromRoute] Guid templateId,
        [FromBody] AddQuestionRequest request,
        [FromServices] AddQuestionHandler template,
        CancellationToken cancellationToken = default)
    {
        var createTemplateResult = await template.Handle(
            templateId,
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
    [HttpGet("{templateId:guid}")]
    public async Task<ActionResult> Get(
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