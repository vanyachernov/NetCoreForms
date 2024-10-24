using CSharpFunctionalExtensions;
using Forms.Application.TemplateDir.GetTemplates;
using Forms.Domain.Shared;

namespace Forms.Application.TemplateDir.GetUserTemplates;

public class GetUserTemplatesHandler(ITemplatesRepository templateRepository)
{
    public async Task<Result<IEnumerable<GetTemplatesResponse>, Error>> Handle(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var templatesResult = await templateRepository.GetByUserId(
            userId,
            cancellationToken);

        if (templatesResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid();
        }

        return templatesResult.Value.ToList();
    }
}