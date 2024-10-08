using CSharpFunctionalExtensions;
using Forms.Application.Template.GetUsers;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Domain.TemplateManagement.Aggregate;

namespace Forms.Application.Template;

public interface ITemplateRepository
{
    /// <summary>
    /// Gets user list.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<IEnumerable<GetUsersResponse>>> GetUsers(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create template.
    /// </summary>
    /// <param name="template">A template.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<Guid>> Create(
        Domain.TemplateManagement.Aggregate.Template template,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Add question to template.
    /// </summary>
    /// <param name="question">Question.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<Guid>> AddQuestion(
        Question question, 
        CancellationToken cancellationToken = default);
}