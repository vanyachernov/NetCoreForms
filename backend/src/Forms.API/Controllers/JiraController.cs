using Forms.API.Controllers.Shared;
using Forms.API.Extensions;
using Forms.Application.JiraDir;
using Forms.Application.JiraDir.CreateTicket;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers;

[ApiController]
[Route("[controller]")]
public class JiraController(IJiraService jiraService) : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> CreateTicket(
        [FromBody] CreateTicketRequest request)
    {
        try
        {
            var newTicketResult = await jiraService.CreateTicketAsync(request);

            if (newTicketResult.IsFailure)
            {
                return newTicketResult.Error.ToResponse();
            }
            
            return Ok(newTicketResult.Value);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserTickets(string email)
    {
        try
        {
            var ticketsResult = await jiraService.GetUserTicketsAsync(email);

            if (ticketsResult.IsFailure)
            {
                return ticketsResult.Error.ToResponse();
            }
            
            return Ok(ticketsResult.Value);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}