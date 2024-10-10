using CSharpFunctionalExtensions;
using Forms.Domain.Shared;
using Forms.Domain.Shared.IDs;
using Forms.Domain.Shared.ValueObjects;
using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.ValueObjects;

namespace Forms.Domain.TemplateManagement.Entities;

public class Question : Shared.Entity<QuestionId>
{
    private readonly List<AnswerOption> _answerOptions;

    private Question(QuestionId id) : base(id)
    {
        _answerOptions = [];
    }

    private Question(
        QuestionId id,
        Title title,
        QuestionType type,
        Order order,
        IsRequired isRequired,
        Template template) : base(id)
    {
        Title = title;
        Type = type;
        Order = order;
        IsRequired = isRequired;
        Template = template;
        _answerOptions = [];
    }

    public Title Title { get; private set; } = default!;
    public QuestionType Type { get; private set; }
    public Order Order { get; private set; } = default!;
    public IsRequired IsRequired { get; private set; } = default!;
    public Template Template { get; private set; } = default!;

    public IReadOnlyCollection<AnswerOption> Options => _answerOptions;
    
    public void AddAnswerOption(AnswerOption option) => _answerOptions.Add(option);

    public static Result<Question> Create(
        QuestionId id, 
        Title title, 
        QuestionType type,
        Order order,
        IsRequired isRequired,
        Template template)
    {
        return new Question(
            id, 
            title, 
            type, 
            order, 
            isRequired, 
            template);
    }
}