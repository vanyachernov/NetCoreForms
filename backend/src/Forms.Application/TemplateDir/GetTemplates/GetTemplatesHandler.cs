using CSharpFunctionalExtensions;
using Forms.Domain.Shared;

namespace Forms.Application.TemplateDir.GetTemplates;

public class GetTemplatesHandler(ITemplatesRepository templateRepository)
{
    public async Task<Result<IEnumerable<GetTemplatesResponse>, Error>> Handle(
        CancellationToken cancellationToken = default)
    {
        var templatesResult = await templateRepository.Get(
            cancellationToken);

        if (templatesResult.IsFailure)
        {
            return Result.Failure<IEnumerable<GetTemplatesResponse>, Error>(
                templatesResult.Error);
        }

        return Result.Success<IEnumerable<GetTemplatesResponse>, Error>(
            templatesResult.Value);
    }
}