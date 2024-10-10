using CSharpFunctionalExtensions;
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
    
    public async Task<Result<Guid, Error>> Create(
        Template template, 
        CancellationToken cancellationToken = default)
    {
        await _templateContext.Templates
            .AddAsync(
                template, 
                cancellationToken);

        await _templateContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success<Guid, Error>(template.Id.Value);
    }

    public async Task<Result<bool, Error>> IsExists(
        Guid templateId, 
        CancellationToken cancellationToken = default)
    {
        var template = await _templateContext.Templates
            .FirstOrDefaultAsync(
                t => t.Id == templateId, 
                cancellationToken);

        var isTemplateExists = template != null;
        
        return isTemplateExists 
            ? Result.Success<bool, Error>(isTemplateExists)
            : Errors.General.NotFound(templateId);
    }

    public async Task<Result<Guid, Error>> AddQuestion(
        Question question, 
        CancellationToken cancellationToken = default)
    {
        await _templateContext.Questions.AddAsync(
            question, 
            cancellationToken);

        return Result.Success<Guid, Error>(question.Id);
    }

    public async Task<Result<IEnumerable<GetQuestionsResponse>, Error>> GetQuestions(
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
            QuestionType = q.Type.ToString(),

            Answers = q.Type == QuestionType.MultipleChoice 
                ? q.Options.Select(o => new AnswerResponseDto
                {
                    AnswerId = o.Id,
                    Answer = new AnswerValueDto(o.AnswerValue.Value),
                    IsCorrect = new IsCorrectDto(o.IsCorrect.Value)
                }).ToList()
                : new List<AnswerResponseDto>(),

            TextAnswer = q.Type == QuestionType.TextAnswer
                ? await _templateContext.Answers
                    .Where(a => a.Instance.Template.Id == templateId)
                    .Where(a => a.TextAnswer != null)
                    .Select(a => a.TextAnswer)
                    .FirstOrDefaultAsync(cancellationToken)
                : null
        }).ToList();
        
        return await Task.WhenAll(questionResponses);
    }
}