using CSharpFunctionalExtensions;
using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.Entities;

namespace Forms.Application.TemplateDir;

public interface ITemplatesRepository
{
    /// <summary>
    /// Create template.
    /// </summary>
    /// <param name="template">A template.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Guid> Create(
        Template template,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Add question to template.
    /// </summary>
    /// <param name="question">Question.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Guid> AddQuestion(
        Question question, 
        CancellationToken cancellationToken = default);
}