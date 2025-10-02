using System.Net;
using ContratacaoService.Application.Errors;

namespace ContratacaoService.Api.Middleware;

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
        catch (NotFoundException nf)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new { error = nf.Message });
        }
        catch (BusinessRuleException br)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsJsonAsync(new { error = br.Message });
        }
    }
}


