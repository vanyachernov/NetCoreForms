using CSharpFunctionalExtensions;
using Forms.Domain.Shared;
using Forms.Domain.Shared.IDs;
using Forms.Domain.Shared.ValueObjects;
using Forms.Domain.TemplateManagement.ValueObjects;

namespace Forms.Application.TemplateDir.Create;

public class CreateTemplateHandler(ITemplatesRepository templateRepository)
{
    public async Task<Result<Guid, Error>> Handle(
        CreateTemplateRequest request,
        CancellationToken cancellationToken = default)
    {
        var templateTitle = Title.Create(request.Title.Value);
        if (templateTitle.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Title");
        }
        
        var templateDescription = Description.Create(request.Description.Value);
        if (templateDescription.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Description");
        }
        
        var templateToCreate = Domain.TemplateManagement.Aggregate.Template.Create(
            TemplateId.NewId,
            templateTitle.Value,
            templateDescription.Value);
        if (templateToCreate.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Template");
        }
        
        var createTemplateResult = await templateRepository.Create(
            templateToCreate.Value,
            cancellationToken);

        return createTemplateResult;
    }
}