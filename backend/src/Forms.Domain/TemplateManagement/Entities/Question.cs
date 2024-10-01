using CSharpFunctionalExtensions;
using Forms.Domain.Shared;
using Forms.Domain.Shared.IDs;
using Forms.Domain.Shared.ValueObjects;

namespace Forms.Domain.TemplateManagement.Entities;

public class Question : Shared.Entity<QuestionId>
{
    private Question(QuestionId id) : base(id) { }

    private Question(
        QuestionId id,
        Title title,
        QuestionType type) : base(id)
    {
        Title = title;
        Type = type;
    }

    public Title Title { get; private set; } = default!;
    public QuestionType Type { get; private set; } = default!;

    public static Result<Question> Create(
        QuestionId id, 
        Title title, 
        QuestionType type)
    {
        return new Question(id, title, type);
    }
}