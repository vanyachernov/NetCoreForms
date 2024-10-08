using CSharpFunctionalExtensions;
using Forms.Application.UserDir.GetUsers;

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
}