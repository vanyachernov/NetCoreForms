using CSharpFunctionalExtensions;
using Forms.Domain.Shared;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.Aggregate;

namespace Forms.Domain.TemplateManagement.Entities;

public class TemplateRoles : Shared.Entity<TemplateRolesId>
{
    private TemplateRoles(TemplateRolesId id) : base(id) { }

    private TemplateRoles(
        TemplateRolesId id,
        Template template,
        User user,
        TemplateRole role) : base(id)
    {
        Template = template;
        TemplateId = template.Id;
        User = user;
        Role = role;
    }

    public TemplateId TemplateId { get; private set; } = default!;
    public Template Template { get; private set; } = default!;
    public User User { get; private set; } = default!;
    public TemplateRole Role { get; private set; }

    public static Result<TemplateRoles> Create(
        TemplateRolesId id,
        Template template,
        User user,
        TemplateRole role)
    {
        return new TemplateRoles(
            id,
            template,
            user,
            role);
    }
}