namespace Forms.Application.TemplateDir.GetQuestions;

public class GetQuestionsHandler(ITemplatesRepository templateRepository)
{
    public async Task<IEnumerable<GetQuestionsResponse>> Handle(
        Guid templateId,
        CancellationToken cancellationToken = default)
    {
        var data = await templateRepository.GetQuestions(
            templateId, 
            cancellationToken);

        return data;
    }
}