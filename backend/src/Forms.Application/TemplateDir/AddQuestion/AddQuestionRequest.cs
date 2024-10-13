using Forms.Application.DTOs;

namespace Forms.Application.TemplateDir.AddQuestion;

public class AddQuestionRequest
{
    public AnswerValueDto Answer { get; set; } 
    public IsCorrectDto IsCorrect { get; set; }
}