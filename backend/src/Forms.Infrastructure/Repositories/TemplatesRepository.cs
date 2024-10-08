using Forms.Application.DTOs;
using Forms.Application.TemplateDir;
using Forms.Application.TemplateDir.GetQuestions;
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
            .Include(q => q.Answers)
            .ToListAsync(cancellationToken);
        
        var questionResponses = questions.Select(q => new GetQuestionsResponse
        {
            QuestionId = q.Id,
            Title = new TitleDto(q.Title.Value),
            Answers = q.Answers.Select(a => new AnswerResponseDto
            {
                AnswerId = a.Id,
                Answer = new AnswerValueDto(a.AnswerValue.Value),
                IsCorrect = new IsCorrectDto(a.IsCorrect.Value)
            }).ToList()
        }).ToList();
        
        return questionResponses;
    }
}