namespace Forms.Application.JiraDir.Shared;

public record Fields
{
    public Project project { get; set; } = new();
    public string summary { get; set; } = default!;
    public string? description { get; set; }
    public IssueType issuetype { get; set; } = new();
    public Assigne assignee { get; set; } = new();
}