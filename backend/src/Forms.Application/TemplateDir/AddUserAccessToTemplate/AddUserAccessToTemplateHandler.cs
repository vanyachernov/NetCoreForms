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
            return Errors.General.NotFound();
        }
        
        var template = templateResult.Value;

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return Errors.General.NotFound();
        }

        var newRoleResult = TemplateRoles.Create(
            TemplateRolesId.NewId,
            template,
            user,
            role);

        if (newRoleResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("New access role");
        }
        
        await _templateRepository.AddUserAccess(
            newRoleResult.Value, 
            cancellationToken);
        
        return newRoleResult.Value.Id.Value;
    }
}