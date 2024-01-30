using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[Route("content")]
[EnableCors("aaa")]
public class ContentController : Controller
{
    private AppDbContext _db;

    public ContentController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("save")]
    public IResult SaveOrUpdate()
    {
        // _db.
        return Results.Ok("Hello world");
    }


    [HttpPost("add1")]
    public IResult add1()
    {
        return Results.Ok("Hello world");
    }
}