namespace Forms.Domain.Shared.IDs;

public record QuestionId
{
    private QuestionId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static QuestionId NewId => new(Guid.NewGuid());

    public static QuestionId NewEmptyId => new(Guid.Empty);

    public static QuestionId Create(Guid id) => new(id);

    public static implicit operator Guid(QuestionId id) => id.Value; 
};