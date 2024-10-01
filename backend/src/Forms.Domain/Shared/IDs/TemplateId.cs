namespace Forms.Domain.Shared.IDs;

public record TemplateId
{
    private TemplateId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static TemplateId NewId => new(Guid.NewGuid());

    public static TemplateId NewEmptyId => new(Guid.Empty);

    public static TemplateId Create(Guid id) => new(id);

    public static implicit operator Guid(TemplateId id) => id.Value;
};