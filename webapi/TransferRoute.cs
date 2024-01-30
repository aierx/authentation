using Microsoft.AspNetCore.Mvc.Routing;

namespace webapi;

public class TransferRoute : DynamicRouteValueTransformer
{
    private string _route = string.Empty;

    public TransferRoute(string route)
    {
        _route = route;
    }

    public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
    {
        if (!values.ContainsKey("dir")) return new ValueTask<RouteValueDictionary>(values);

        var dir = (string)values["dir"];
        return new ValueTask<RouteValueDictionary>(values);
    }
}