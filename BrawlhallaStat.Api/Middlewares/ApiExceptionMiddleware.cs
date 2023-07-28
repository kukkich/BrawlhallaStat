using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Middlewares;

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionMiddleware> _logger;

    public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException ex)
        {
            _logger.LogWarning(ex.Message);

            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync($"{{'error': {ex.Message}}}");
        }
    }

}