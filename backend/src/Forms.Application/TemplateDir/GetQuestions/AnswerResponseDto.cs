using Forms.Application.DTOs;

namespace Forms.Application.TemplateDir.GetQuestions;

public record AnswerResponseDto
{
    public Guid AnswerId { get; set; }
    public AnswerValueDto Answer { get; set; }
    public IsCorrectDto IsCorrect { get; set; }
};