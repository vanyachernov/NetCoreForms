using Forms.Application.DTOs;

namespace Forms.Application.TemplateDir.GetQuestions;

public record GetQuestionsResponse
{
    public Guid QuestionId { get; set; }
    public TitleDto Title { get; set; }
    public List<AnswerResponseDto> Answers { get; set; } = [];
};