using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[Route("content")]
[EnableCors("aaa")]
public class ContentController: Controller
{

    [HttpPost("add")]
    public IResult add()
    {
        return Results.Ok( "Hello world");

    }
    
    
    [HttpPost("add1")]
    public IResult add1()
    {
        return Results.Ok( "Hello world");

    }
    
}