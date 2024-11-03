using Forms.API.Extensions;
using Forms.Application.UserDir.CreateContact;
using Forms.Application.UserDir.СreateAccount;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers.Shared;

[ApiController]
[Route("[controller]")]
public class SalesforceController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> GetInstanceUrl(
        [FromBody] CreateAccountRequest request,
        [FromServices] CreateAccountHandler accountHandler,
        [FromServices] CreateContactHandler contactHandler)
    {
        try
        {
            var accountIdentifierResult = await accountHandler.Handle(request.AccountName);
        
            if (accountIdentifierResult.IsFailure)
            {
                return accountIdentifierResult.Error.ToResponse();
            }

            var contactIdentifierResult = await contactHandler.Handle(
                accountIdentifierResult.Value,
                request);

            if (contactIdentifierResult.IsFailure)
            {
                return contactIdentifierResult.Error.ToResponse();
            }

            return Ok(new
            {
                AccountId = accountIdentifierResult.Value,
                ContactId = contactIdentifierResult.Value
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}