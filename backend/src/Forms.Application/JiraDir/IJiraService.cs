using CSharpFunctionalExtensions;
using Forms.Application.JiraDir.CreateTicket;
using Forms.Domain.Shared;

namespace Forms.Application.JiraDir;

public interface IJiraService
{
    Task<Result<CreateTicketResponse, Error>> CreateTicketAsync(CreateTicketRequest request);

    Task<List<CreateTicketResponse>> GetUserTicketsAsync(string userId);

    Task<Result<bool, Error>> CreateUserAsync(
        string email,
        string displayName);

    Task<Result<bool, Error>> UserExistsAsync(string email);
}