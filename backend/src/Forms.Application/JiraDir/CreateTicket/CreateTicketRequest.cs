namespace Forms.Application.JiraDir.CreateTicket;

public class CreateTicketRequest
{
    public string Summary { get; set; }
    public string Description { get; set; }
    public string IssueType { get; set; }
    public string UserEmail { get; set; }
    public string DisplayName { get; set; }
    public string ProjectKey { get; set; }
}
