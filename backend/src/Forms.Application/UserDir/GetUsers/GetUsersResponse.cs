using Forms.Application.DTOs;

namespace Forms.Application.UserDir.GetUsers;

public record GetUsersResponse
{
    public Guid Id { get; set; }
    public FullNameDto FullName { get; set; }
    public EmailDto Email { get; set; }
};