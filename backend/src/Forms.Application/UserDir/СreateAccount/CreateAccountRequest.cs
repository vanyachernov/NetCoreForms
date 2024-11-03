namespace Forms.Application.UserDir.СreateAccount;

public record CreateAccountRequest
{
    public string AccountName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
};