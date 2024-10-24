using CSharpFunctionalExtensions;
using Forms.Application.TemplateDir.GetQuestions;
using Forms.Application.TemplateDir.GetTemplates;
using Forms.Domain.Shared;
using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.Entities;

namespace Forms.Application.TemplateDir;

public interface ITemplatesRepository
{
    /// <summary>
    /// Return a list of templates.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{Template}"/>.</returns>
    Task<Result<IEnumerable<GetTemplatesResponse>, Error>> Get(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Return a template by identifier.
    /// </summary>
    /// <param name="templateId">A template identifier.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{Template}"/>.</returns>
    Task<Result<Template, Error>> GetById(
        Guid templateId,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Return a template by user identifier.
    /// </summary>
    /// <param name="userId">A user identifier.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{Template}"/>.</returns>
    Task<Result<IEnumerable<GetTemplatesResponse>, Error>> GetByUserId(
        Guid userId,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Create template.
    /// </summary>
    /// <param name="template">A template.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<Guid, Error>> Create(
        Template template,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Delete template.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Template identifier.</returns>
    Task<Result<Guid, Error>> Delete(
        Guid templateId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if template exists.
    /// </summary>
    /// <param name="templateId">A template identifier.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    Task<Result<bool, Error>> IsExists(
        Guid templateId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Add question to template.
    /// </summary>
    /// <param name="question">Question.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<Guid, Error>> AddQuestion(
        Question question, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Return a list of template's questions.
    /// </summary>
    /// <param name="templateId">A template identifier.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<IEnumerable<GetQuestionsResponse>, Error>> GetQuestions(
        Guid templateId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Add user access to template.
    /// </summary>
    /// <param name="roles">Template roles.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<Guid, Error>> AddUserAccess(
        TemplateRoles roles, 
        CancellationToken cancellationToken = default);
}