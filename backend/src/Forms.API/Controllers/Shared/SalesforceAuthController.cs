using Forms.Application.DTOs;
using Forms.Application.UserDir.CreateContact;
using Forms.Application.UserDir.СreateAccount;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers.Shared;

[ApiController]
[Route("[controller]")]
public class SalesforceAuthController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> GetInstanceUrl(
        [FromBody] CreateAccountRequest request,
        [FromServices] CreateAccountHandler accountHandler,
        [FromServices] CreateContactHandler contactHandler)
    {
        try
        {
            var accountId = await accountHandler.Handle(request.AccountName);
            
            var contactId = await contactHandler.Handle(
                accountId.Value,
                request);
            
            return Ok(new
            {
                AccountId = accountId, 
                ContactId = contactId
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}