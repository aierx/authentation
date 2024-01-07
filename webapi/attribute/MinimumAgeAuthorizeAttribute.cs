using Microsoft.AspNetCore.Authorization;

namespace webapi.attribute;

public class MinimumAgeAuthorizeAttribute : AuthorizeAttribute
{
    private const string POLICY_PREFIX = "MinimumAge";

    public MinimumAgeAuthorizeAttribute(int age)
    {
        Age = age;
    }

    // Get or set the Age property by manipulating the underlying Policy property
    public int Age
    {
        get
        {
            if (int.TryParse(Policy.Substring(POLICY_PREFIX.Length), out var age)) return age;
            return default;
        }
        set => Policy = $"{POLICY_PREFIX}{value.ToString()}";
    }
}