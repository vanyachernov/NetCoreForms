using Forms.Application.DTOs;
using Forms.Application.TemplateDir;
using Forms.Application.TemplateDir.GetQuestions;
using Forms.Domain.Shared;
using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace Forms.Infrastructure.Repositories;

public class TemplatesRepository : ITemplatesRepository
{
    private readonly TemplateDbContext _templateContext;

    public TemplatesRepository(TemplateDbContext context)
    {
        _templateContext = context;
    }
    
    public async Task<Guid> Create(
        Template template, 
        CancellationToken cancellationToken = default)
    {
        await _templateContext.Templates
            .AddAsync(
                template, 
                cancellationToken);

        await _templateContext.SaveChangesAsync(cancellationToken);
        
        return template.Id.Value;
    }

    public async Task<bool> IsExists(Guid templateId, CancellationToken cancellationToken = default)
    {
        var template = await _templateContext.Templates
            .FirstOrDefaultAsync(
                t => t.Id == templateId, 
                cancellationToken);

        return template != null;
    }

    public async Task<Guid> AddQuestion(
        Question question, 
        CancellationToken cancellationToken = default)
    {
        await _templateContext.Questions.AddAsync(
            question, 
            cancellationToken);

        return question.Id;
    }

    public async Task<IEnumerable<GetQuestionsResponse>> GetQuestions(
        Guid templateId,
        CancellationToken cancellationToken = default)
    {
        var questions = await _templateContext.Questions
            .Where(q => q.Template.Id == templateId)
            .Include(q => q.Options)
            .ToListAsync(cancellationToken);
        
        var questionResponses = questions.Select(async q => new GetQuestionsResponse
        {
            QuestionId = q.Id,
            Title = new TitleDto(q.Title.Value),
            QuestionType = q.Type.ToString(), // Convert enum to string

            Answers = q.Type == QuestionType.MultipleChoice 
                ? q.Options.Select(o => new AnswerResponseDto
                {
                    AnswerId = o.Id,
                    Answer = new AnswerValueDto(o.AnswerValue.Value),
                    IsCorrect = new IsCorrectDto(o.IsCorrect.Value)
                }).ToList()
                : new List<AnswerResponseDto>(),

            // Fetching TextAnswer for text questions from related Answers
            TextAnswer = q.Type == QuestionType.TextAnswer
                ? await _templateContext.Answers
                    .Where(a => a.Instance.Template.Id == templateId) // Adjusted to use the template
                    .Where(a => a.TextAnswer != null) // Ensure we only get answers with text
                    .Select(a => a.TextAnswer)
                    .FirstOrDefaultAsync(cancellationToken)
                : null
        }).ToList();
        
        return await Task.WhenAll(questionResponses);
    }
}