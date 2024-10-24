using Forms.API.Response;
using Forms.Domain.Shared;

namespace Forms.API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var error = new ResponseError(
                "server.internal", 
                e.Message, 
                "Cancellation request.");
            
            var envelope = Envelope.Error([error]);
            
            await context.Response.WriteAsJsonAsync(envelope);
        }
    }
}