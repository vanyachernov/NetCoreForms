using Forms.Application.DTOs;

namespace Forms.Application.Template.GetUsers;

public record GetUsersResponse
{
    public Guid Id { get; set; }
    public FullNameDto FullName { get; set; }
    public EmailDto Email { get; set; }
};