using Forms.API.Response;
using Microsoft.AspNetCore.Mvc;

namespace Forms.API.Controllers.Shared;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);
        
        return new OkObjectResult(envelope);
    }
}