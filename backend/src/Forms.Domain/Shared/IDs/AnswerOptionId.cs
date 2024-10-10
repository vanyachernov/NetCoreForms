namespace Forms.Domain.Shared.IDs;

public record AnswerOptionId
{
    private AnswerOptionId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static AnswerOptionId NewId => new(Guid.NewGuid());

    public static AnswerOptionId NewEmptyId => new(Guid.Empty);

    public static AnswerOptionId Create(Guid id) => new(id);

    public static implicit operator Guid(AnswerOptionId id) => id.Value; 
}