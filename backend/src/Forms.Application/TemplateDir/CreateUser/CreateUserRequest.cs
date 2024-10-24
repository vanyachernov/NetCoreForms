using Forms.Application.DTOs;

namespace Forms.Application.TemplateDir.CreateUser;

public class CreateUserRequest
{
    public FullNameDto FullName { get; set; }
    public EmailDto Email { get; set; }
    public PasswordDto Password { get; set; }
}