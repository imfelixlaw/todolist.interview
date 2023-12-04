using Azure;
using System.Net.Http.Headers;
using System.Text;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services.Managers;

public class BasicAuthMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            // authenticate credentials with user service and attach user to http context

            var user = await userService.Authenticate(username, password);
            if(string.IsNullOrEmpty(user)) 
            {
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }
            context.Items["User"] = username;
        }
        catch
        {
            // do nothing if invalid auth header
            // user is not attached to context so request won't have access to secure routes
        }

        await _next(context);
    }

    //public async Task Invoke(HttpContext context, IUserService userService)
    //{
    //    string authHeader = context.Request.Headers["Authorization"];
    //    if (authHeader != null && authHeader.StartsWith("Basic"))
    //    {
    //        // Extract credentials
    //        string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
    //        Encoding encoding = Encoding.GetEncoding("iso-8859-1");
    //        string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
    //
    //        int seperatorIndex = usernamePassword.IndexOf(':');
    //
    //        var username = usernamePassword[..seperatorIndex];
    //        var password = usernamePassword[(seperatorIndex + 1)..];
    //
    //        if (await userService.Authenticate(username, password) != Guid.Empty)
    //        {
    //            await _next.Invoke(context);
    //        }
    //        else
    //        {
    //            context.Response.StatusCode = 401; // Unauthorized
    //            return;
    //        }
    //    }
    //    //else
    //    //{
    //    //    // No authorization header
    //    //    context.Response.StatusCode = 401; // Unauthorized
    //    //    return;
    //    //}
    //}
}
