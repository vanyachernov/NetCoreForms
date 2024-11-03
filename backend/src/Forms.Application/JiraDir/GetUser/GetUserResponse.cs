namespace Forms.Application.JiraDir.GetUser;

public record GetUserResponse
{
    public string accountId { get; set; } = string.Empty;
    public string emailAddress { get; set; } = string.Empty;
};