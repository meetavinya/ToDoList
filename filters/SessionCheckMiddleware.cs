using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class SessionCheckMiddleware
{
    private readonly RequestDelegate _next;

    public SessionCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value.ToLower();

        // Allow public paths (login, register, static files, etc.)
        var allowedPaths = new[]
        {
            "/My/login",
            "/My/register"
        };

        bool isAllowed = allowedPaths.Any(p => path.StartsWith(p));

        if (!isAllowed)
        {
            var username = context.Session.GetString("username");

            if (string.IsNullOrEmpty(username))
            {
                context.Response.Redirect("/My/login");
                return;
            }
        }

        await _next(context);
    }
}
