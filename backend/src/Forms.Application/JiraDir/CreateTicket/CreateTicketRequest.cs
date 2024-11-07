namespace Forms.Application.JiraDir.CreateTicket;

public record CreateTicketRequest
{
    public string Summary { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Priority { get; set; }
    public string Link { get; set; }
    public string? TemplateName { get; set; }
}