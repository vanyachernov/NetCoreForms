using CSharpFunctionalExtensions;
using Forms.Domain.Shared.IDs;

namespace Forms.Domain.TemplateManagement.Entities;

public class Answer : Shared.Entity<AnswerId>
{
    private Answer(AnswerId id) : base(id) { }

    public Instance Instance { get; private set; } = default!;
    public AnswerOption? SelectedAnswerOption { get; private set; }
    public string? TextAnswer { get; private set; }
    
    public static Result<Answer> CreateTextAnswer(
        AnswerId id,
        Instance instance,
        string textAnswer)
    {
        return new Answer(id)
        {
            Instance = instance,
            TextAnswer = textAnswer
        };
    }
    
    public static Result<Answer> CreateOptionAnswer(
        AnswerId id,
        Instance instance,
        AnswerOption selectedOption)
    {
        return new Answer(id)
        {
            Instance = instance,
            SelectedAnswerOption = selectedOption
        };
    }
}
