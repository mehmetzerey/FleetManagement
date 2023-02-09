using System.Net;
using FleetManagement.Application;
using FleetManagement.Application.Interfaces;

namespace FleetManagement.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IAppLogger<ExceptionMiddleware> _logger)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync(new ErrorDetail()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            }.ToString());
        }
    }
}
