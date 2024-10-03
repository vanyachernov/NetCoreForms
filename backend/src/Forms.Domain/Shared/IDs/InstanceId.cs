namespace Forms.Domain.Shared.IDs;

public record InstanceId
{
    private InstanceId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static InstanceId NewId => new(Guid.NewGuid());

    public static InstanceId NewEmptyId => new(Guid.Empty);

    public static InstanceId Create(Guid id) => new(id);

    public static implicit operator Guid(InstanceId id) => id.Value; 
};