using CSharpFunctionalExtensions;
using Forms.Domain.Shared.IDs;
using Forms.Domain.Shared.ValueObjects;
using Forms.Domain.TemplateManagement.ValueObjects;

namespace Forms.Application.Template.Create;

public class CreateTemplateHandler(ITemplateRepository templateRepository)
{
    public async Task<Result<Guid>> Handle(
        CreateTemplateRequest request,
        CancellationToken cancellationToken = default)
    {
        var templateTitle = Title.Create(request.Title.Value);
        if (templateTitle.IsFailure)
        {
            return Result.Failure<Guid>("Title is invalid!");
        }
        
        var templateDescription = Description.Create(request.Description.Value);
        if (templateDescription.IsFailure)
        {
            return Result.Failure<Guid>("Description is invalid!");
        }
        
        var templateToCreate = Domain.TemplateManagement.Aggregate.Template.Create(
            TemplateId.NewId,
            templateTitle.Value,
            templateDescription.Value);
        if (templateToCreate.IsFailure)
        {
            return Result.Failure<Guid>("Template hasn't been created!");
        }
        
        var createTemplateResult = await templateRepository.Create(
            templateToCreate.Value,
            cancellationToken);

        return createTemplateResult.Value;
    }
}