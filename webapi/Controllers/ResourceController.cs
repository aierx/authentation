using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;

[Route("resource")]
public class ResourceController
{
    private readonly AppDbContext _db;

    public ResourceController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("add")]
    public IResult Add([FromBody] ResourceVo resourceVo)
    {
        return Results.Ok("ok");
    }

    [HttpGet("query")]
    public List<ResourcePo> Query()
    {
        return null;

    }

    [HttpGet("queryByName")]
    public ResourcePo? QueryByName([FromQuery] string name)
    {
        return null;

    }
}