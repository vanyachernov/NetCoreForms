using CSharpFunctionalExtensions;
using Forms.Domain.Shared;
using Forms.Domain.Shared.IDs;
using Forms.Domain.Shared.ValueObjects;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Domain.TemplateManagement.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Forms.Application.TemplateDir.Create;

public class CreateTemplateHandler
{
    private readonly ITemplatesRepository _templateRepository;
    private readonly UserManager<User> _userManager;

    public CreateTemplateHandler(
        ITemplatesRepository templateRepository,
        UserManager<User> userManager)
    {
        _templateRepository = templateRepository;
        _userManager = userManager;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        Guid userId,
        CreateTemplateRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user == null)
        {
            return Errors.General.NotFound();
        }
        
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
            user,
            templateTitle.Value,
            templateDescription.Value);
        
        if (templateToCreate.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Template");
        }
        
        var createTemplateResult = await _templateRepository.Create(
            templateToCreate.Value,
            cancellationToken);

        return createTemplateResult;
    }
}