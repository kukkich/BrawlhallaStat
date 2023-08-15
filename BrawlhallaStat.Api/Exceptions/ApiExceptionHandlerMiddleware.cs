namespace BrawlhallaStat.Api.Exceptions;

public class ApiExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionHandlerMiddleware> _logger;

    public ApiExceptionHandlerMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlerMiddleware> logger)
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
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync($"{{\"error\": \"{ex.Message}\"}}");
        }
    }

}