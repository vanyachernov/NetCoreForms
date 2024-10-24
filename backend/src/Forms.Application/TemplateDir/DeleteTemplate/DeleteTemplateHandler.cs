using CSharpFunctionalExtensions;
using Forms.Domain.Shared;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.AspNetCore.Identity;

namespace Forms.Application.TemplateDir.DeleteTemplate;

public class DeleteTemplateHandler
{
    private readonly ITemplatesRepository _templateRepository;
    private readonly UserManager<User> _userManager;

    public DeleteTemplateHandler(
        ITemplatesRepository templateRepository,
        UserManager<User> userManager)
    {
        _templateRepository = templateRepository;
        _userManager = userManager;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        Guid userId,
        Guid templateId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user == null)
        {
            return Errors.General.NotFound();
        }
        
        var templateExistsResult = await _templateRepository.IsExists(
            templateId, 
            cancellationToken);
        
        if (templateExistsResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Template identifier");
        }

        var deleteTemplateResult = await _templateRepository.Delete(
            templateId,
            cancellationToken);

        if (deleteTemplateResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("Delete template");
        }
        
        return deleteTemplateResult.Value;
    }
}