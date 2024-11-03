using Forms.Application.JiraDir.Shared;

namespace Forms.Application.JiraDir.CreateTicket;

public record JiraIssueRequest
{
    public Fields fields { get; set; } = new();
}
