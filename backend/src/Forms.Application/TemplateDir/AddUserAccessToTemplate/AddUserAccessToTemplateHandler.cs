using CSharpFunctionalExtensions;
using Forms.Domain.Shared;
using Forms.Domain.Shared.IDs;
using Forms.Domain.Shared.ValueObjects;
using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Domain.TemplateManagement.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Forms.Application.TemplateDir.AddUserAccessToTemplate;

public class AddUserAccessToTemplateHandler
{
    private readonly ITemplatesRepository _templateRepository;
    private readonly UserManager<User> _userManager;
    
    public AddUserAccessToTemplateHandler(
        ITemplatesRepository templateRepository,
        UserManager<User> userManager)
    {
        _templateRepository = templateRepository;
        _userManager = userManager;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        Guid userId,
        Guid templateId,
        TemplateRole role,
        CancellationToken cancellationToken = default)
    {
        var templateResult = await _templateRepository.GetById(
            templateId, 
            cancellationToken);

        if (templateResult.IsFailure)
        {
            Errors.General.NotFound();
        } ;

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            Errors.General.NotFound();
        }

        var templateResultValue = templateResult.Value;

        var newTemplateResult = Template.Create(
            TemplateId.NewId, 
            user,
            Title.Create(templateResultValue.Title.Value).Value,
            Description.Create(templateResultValue.Description.Value).Value);

        if (newTemplateResult.IsFailure)
        {
            Errors.General.ValueIsInvalid("Template");
        }
        
        var newRoleResult = TemplateRoles.Create(
            TemplateRolesId.NewId, 
            newTemplateResult.Value, 
            user, 
            role);

        if (newRoleResult.IsFailure)
        {
            Errors.General.ValueIsInvalid("New access role");
        }
        
        await _templateRepository.AddUserAccess(
            newRoleResult.Value, 
            cancellationToken);

        var newRole = newRoleResult.Value;
        
        return newRole.Id.Value;
    }
}