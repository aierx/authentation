using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;

[Route("role")]
[EnableCors("aaa")]
public class RoleController
{
    private readonly AppDbContext _db;

    public RoleController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("add")]
    public IResult add([FromBody] RoleVo roleVo)
    {
        _db.Role.Add(new RolePo { name = roleVo.Name });
        _db.SaveChanges();
        return Results.Ok();
    }

    [HttpGet("query")]
    public List<RoleVo> query()
    {
        var userVos = (from rolePo in _db.Role select new RoleVo { Name = rolePo.name, Id = rolePo.Id }).ToList();
        return userVos;
    }

    [HttpGet("queryByName")]
    public RoleVo queryByName([FromQuery] string name)
    {
        return (from rolePo in _db.Role
                where rolePo.name == name
                select new RoleVo { Name = rolePo.name, Id = rolePo.Id })
            .FirstOrDefault(new RoleVo());
    }
}