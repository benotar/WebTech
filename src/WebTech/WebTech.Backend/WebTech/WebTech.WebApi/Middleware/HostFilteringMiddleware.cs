namespace WebTech.WebApi.Middleware;

public class HostFilteringMiddleware
{
    private readonly RequestDelegate _next;
    
    private readonly IReadOnlyCollection<string> _allowedHosts;

    public HostFilteringMiddleware(RequestDelegate next)
    {
        _next = next;
        
        _allowedHosts = new List<string>
        {
            "bg-local.com",
            "api.bg-local.net"
        };
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var host = context.Request.Host.Host;

        if (!_allowedHosts.Contains(host))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }

        await _next.Invoke(context);
    }
}

public static class HostFilteringMiddlewareExtensions
{
    public static IApplicationBuilder UseHostFilteringMiddleware(this IApplicationBuilder app)
        => app.UseMiddleware<HostFilteringMiddleware>();
}