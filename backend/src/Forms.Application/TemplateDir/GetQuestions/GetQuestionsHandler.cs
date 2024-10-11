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
            return Result.Failure<IEnumerable<GetQuestionsResponse>, Error>(
                templateExistsResult.Error);
        }
        
        if (!templateExistsResult.Value)
        {
            return Result.Failure<IEnumerable<GetQuestionsResponse>, Error>(
                Errors.General.NotFound(templateId));
        }
        
        var questionsResult = await templateRepository.GetQuestions(
            templateId, 
            cancellationToken);
        
        if (questionsResult.IsFailure)
        {
            return Result.Failure<IEnumerable<GetQuestionsResponse>, Error>(
                questionsResult.Error);
        }
        
        return Result.Success<IEnumerable<GetQuestionsResponse>, Error>(
            questionsResult.Value);
    }

}