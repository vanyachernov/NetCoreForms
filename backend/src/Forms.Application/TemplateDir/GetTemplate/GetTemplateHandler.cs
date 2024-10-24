using CSharpFunctionalExtensions;
using Forms.Application.DTOs;
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

        var template = templateResult.Value;
        
        var templateDto = new GetTemplatesResponse
        {
            Id = template.Id,
            Owner = new UserDto(
                template.Owner.Id, 
                template.Owner.Email,
                new FullNameDto(
                    template.Owner.FullName.LastName, 
                    template.Owner.FullName.FirstName)),
            Title = new TitleDto(template.Title.Value),
            Description = new DescriptionDto(template.Description.Value)
        };

        return templateDto;
    }
}