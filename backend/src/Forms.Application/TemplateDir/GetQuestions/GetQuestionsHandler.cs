using CSharpFunctionalExtensions;

namespace Forms.Application.TemplateDir.GetQuestions;

public class GetQuestionsHandler(ITemplatesRepository templateRepository)
{
    public async Task<Result<IEnumerable<GetQuestionsResponse>>> Handle(
        Guid templateId,
        CancellationToken cancellationToken = default)
    {
        var existingTemplate = await templateRepository.IsExists(
            templateId, 
            cancellationToken);

        if (!existingTemplate)
        {
            return Result.Failure<IEnumerable<GetQuestionsResponse>>("Template not exists!");
        }
        
        var questions = await templateRepository.GetQuestions(
            templateId, 
            cancellationToken);

        return Result.Success(questions);
    }
}