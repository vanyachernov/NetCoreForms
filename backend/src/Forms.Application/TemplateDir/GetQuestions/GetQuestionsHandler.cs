using CSharpFunctionalExtensions;
using Forms.Domain.Shared;

namespace Forms.Application.TemplateDir.GetQuestions;

public class GetQuestionsHandler(ITemplatesRepository templateRepository)
{
    public async Task<Result<IEnumerable<GetQuestionsResponse>, Error>> Handle(
        Guid templateId,
        CancellationToken cancellationToken = default)
    {
        var templateExistsResult = await templateRepository.IsExists(
            templateId, 
            cancellationToken);
        
        if (templateExistsResult.IsFailure)
        {
            return Errors.General.NotFound();
        }
        
        var questionsResult = await templateRepository.GetQuestions(
            templateId, 
            cancellationToken);
        
        if (questionsResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Questions");
        }
        
        return questionsResult.Value.ToList();
    }
}