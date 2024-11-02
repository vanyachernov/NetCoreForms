using Forms.Application.JiraDir.CreateTicket;

namespace Forms.Application.JiraDir.SearchTickets;

public class SearchTicketsResponse
{
    public List<CreateTicketResponse> Issues { get; set; }
}