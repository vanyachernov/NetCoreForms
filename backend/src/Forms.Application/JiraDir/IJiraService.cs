using CSharpFunctionalExtensions;
using Forms.Application.JiraDir.CreateTicket;
using Forms.Domain.Shared;

namespace Forms.Application.JiraDir;

public interface IJiraService
{
    Task<Result<CreateTicketResponse, Error>> CreateTicketAsync(CreateTicketRequest request);

    Task<Result<List<object>, Error>> GetUserTicketsAsync(string email);

    Task<Result<string, Error>> CreateUserAsync(string email);

    Task<Result<bool, Error>> UserExistsAsync(string email);

    Task<Result<string, Error>> GetUserAccountIdAsync(string email);
}