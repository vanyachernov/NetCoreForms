namespace Forms.Domain.Shared.IDs;

public record AnswerId
{
    private AnswerId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static AnswerId NewId => new(Guid.NewGuid());

    public static AnswerId NewEmptyId => new(Guid.Empty);

    public static AnswerId Create(Guid id) => new(id);

    public static implicit operator Guid(AnswerId id) => id.Value; 
};