using Microsoft.AspNetCore.Authorization;

namespace webapi;

public class MinimumAgeHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        var requirements = context.PendingRequirements.ToList();
        foreach (var requirement in requirements)
        {
            if (requirement is not MinimumAgeRequirement ageRequirement) continue;
            // 角色判断
            if (context.User.Claims.Where(e => e.Type.Equals("Role")).Any(e => e.Value == ageRequirement.Permission))
            {
                context.Succeed(ageRequirement);
                break;
            }

            // 权限判断
            if (context.User.Claims.Where(e => e.Type.Equals("Permission")).Any(e => e.Value == ageRequirement.Role))
            {
                context.Succeed(ageRequirement);
                break;
            }
        }

        context.Fail();
        return Task.CompletedTask;
    }
}