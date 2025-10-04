using System.Net;

namespace PropostaService.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException arg)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { error = arg.Message });
        }
        catch (KeyNotFoundException knf)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new { error = knf.Message });
        }
    }
}
