namespace Forms.Application.JiraDir.CreateTicket;

public record CreateTicketRequest
{
    public string Summary { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string CurrentUrl { get; set; }
}