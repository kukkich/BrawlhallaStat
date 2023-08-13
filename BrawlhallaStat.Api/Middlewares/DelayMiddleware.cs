namespace BrawlhallaStat.Api.Middlewares;

public class DelayMiddleware
{
    private readonly RequestDelegate _next;

    public DelayMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await Task.Delay(1500);
        await _next.Invoke(context);
    }
}