using CSharpFunctionalExtensions;
using Forms.Application.UserDir.GetUsers;
using Forms.Domain.Shared;
using Forms.Domain.TemplateManagement.Entities;

namespace Forms.Application.UserDir;

public interface IUsersRepository
{
    /// <summary>
    /// Gets user list.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<IEnumerable<GetUsersResponse>>> GetUsers(
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets role for a specific user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>User role.</returns>
    Task<Result<string, Error>> GetUserRole(
        Guid userId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create new user.
    /// </summary>
    /// <param name="user">User model.</param>
    /// <param name="password">Desired account password.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>New user's identifier.</returns>
    public Task<Result<Guid, Error>> Register(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}