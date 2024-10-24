using CSharpFunctionalExtensions;
using Forms.Application.DTOs;
using Forms.Domain.Shared;
using Forms.Domain.TemplateManagement.Entities;

namespace Forms.Application.UserDir;

public interface ITokensRepository
{
    /// <summary>
    /// Create new token for specify user.
    /// </summary>
    /// <param name="user">User model.</param>
    /// <param name="populateExp">Populate expiry date.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Access Token.</returns>
    Task<Result<TokenDto, Error>> CreateToken(
        User user,
        bool populateExp, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create new refresh token for spicify user.
    /// </summary>
    /// <param name="user">User model.</param>
    /// <param name="tokenDto">Token DTO.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Refresh Token.</returns>
    Task<Result<TokenDto, Error>> RefreshToken(
        User user,
        TokenDto tokenDto, 
        CancellationToken cancellationToken = default);
}