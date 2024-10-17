using CSharpFunctionalExtensions;
using Forms.Application.TemplateDir.GetTemplates;
using Forms.Domain.Shared;

namespace Forms.Application.TemplateDir.GetTemplate;

public class GetTemplateHandler(ITemplatesRepository templateRepository)
{
    public async Task<Result<GetTemplatesResponse, Error>> Handle(
        Guid templateId,
        CancellationToken cancellationToken = default)
    {
        var templateResult = await templateRepository.GetById(
            templateId,
            cancellationToken);

        if (templateResult.IsFailure)
        {
            return Errors.General.NotFound();
        }

        return templateResult.Value;
    }
}