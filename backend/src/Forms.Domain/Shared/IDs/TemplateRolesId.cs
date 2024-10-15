namespace Forms.Domain.Shared.IDs;

public record TemplateRolesId
{
    private TemplateRolesId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static TemplateRolesId NewId => new(Guid.NewGuid());

    public static TemplateRolesId NewEmptyId => new(Guid.Empty);

    public static TemplateRolesId Create(Guid id) => new(id);

    public static implicit operator Guid(TemplateRolesId id) => id.Value;
};