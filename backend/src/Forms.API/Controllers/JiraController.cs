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
            var response = await jiraService.CreateTicketAsync(request);

            if (response.IsFailure)
            {
                return response.Error.ToResponse();
            }
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при создании тикета: {ex.Message}");
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserTickets(
        string userId)
    {
        try
        {
            var tickets = await jiraService.GetUserTicketsAsync(userId);
            
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка при получении тикетов пользователя: {ex.Message}");
        }
    }
}