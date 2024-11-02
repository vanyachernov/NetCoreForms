namespace Forms.Application.JiraDir.Shared;

public class Fields
{
    public string Summary { get; set; } = default!;
    public IssueType IssueType { get; set; } = new IssueType { Id = "10001" };
    public string? Description { get; set; }
    public Priority Priority { get; set; } = default!;
}