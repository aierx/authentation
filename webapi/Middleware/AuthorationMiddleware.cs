using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace webapi.Middleware;

public class MyAuthorizationMiddleware : IMiddleware
{
    private readonly AppDbContext _db;

    private readonly ILogger<MyAuthorizationMiddleware> _logger;

    public MyAuthorizationMiddleware(ILogger<MyAuthorizationMiddleware> logger, AppDbContext db)
    {
        _db = db;
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string path = context.Request.Path;

        var resource = _db.Resource.Include(resourcePo => resourcePo.RolePos)
            .Include(resourcePo => resourcePo.PermissionPos).FirstOrDefault(e => e.name == path);
        if (resource != null)
        {
            var roles = context.User.Claims.Where(e => e.Type.Equals("Role")).Select(e => e.Value).ToList();
            if (resource.RolePos.Count != 0 && !resource.RolePos.All(e => roles.Contains(e.name)))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { statusCode = 400, value = "没有权限" }));
                return;
            }

            // if (resource.PermissionPos.Count!=0&&!)
            // {
            //     
            // }
        }

        await next(context);
    }
}