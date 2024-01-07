using Microsoft.AspNetCore.Authorization;

namespace webapi;

public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public MinimumAgeRequirement(int result)
    {
        MinimumAge = result;
    }

    public int MinimumAge { get; set; }
    public string Role { get; set; }
    public string Permission { get; set; }
}