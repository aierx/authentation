using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Features;

namespace webapi.authentication;

public class MyAuthenticationHandler<TAuthenticationProperties> : IAuthenticationSignInHandler
{
    public AuthenticationScheme Scheme { get; private set; }

    private HttpContext Context { get; set; }

    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    {
        Scheme = scheme;
        Context = context;
        return Task.CompletedTask;
    }

    public Task<AuthenticateResult> AuthenticateAsync()
    {
        throw new NotImplementedException();
    }

    public Task ChallengeAsync(AuthenticationProperties? properties)
    {
        Context.Response.WriteAsJsonAsync(new { Code = 401, Message = "请登入", Result = string.Empty });
        return Task.CompletedTask;
    }
    
    public Task ForbidAsync(AuthenticationProperties? properties)
    {
        Context.Response.WriteAsJsonAsync(new { Code = 403, Message = "没有权限", Result = string.Empty });
        return Task.CompletedTask;
    }

    public Task SignOutAsync(Microsoft.AspNetCore.Authentication.AuthenticationProperties? properties)
    {
        throw new NotImplementedException();
    }

    public Task SignInAsync(ClaimsPrincipal user, Microsoft.AspNetCore.Authentication.AuthenticationProperties? properties)
    {
        throw new NotImplementedException();
    }
    
}