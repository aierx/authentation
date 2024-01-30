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
    public IResult Add([FromBody] RoleVo roleVo)
    {
        if (_db.Role.Any(e => e.name == roleVo.Name))
        {
            return Results.Problem("角色已存在");
        }
        _db.Role.Add(new RolePo { name = roleVo.Name });
        _db.SaveChanges();
        return Results.Ok();
    }
    
    [HttpPost("deleteByName")]
    public IResult DeleteByName([FromBody]string name)
    {
        RolePo rolePo = _db.Role.FirstOrDefault(e => e.name==name);
        if (rolePo==null)
        {
            return Results.Problem("角色不存在");
        }

        _db.Role.Remove(rolePo);
        _db.SaveChanges();
        return Results.Ok("删除成功");
    }
    
    [HttpGet("queryAll")]
    public List<RoleVo> Query()
    {
        var userVos = (from rolePo in _db.Role select new RoleVo { Name = rolePo.name}).ToList();
        return userVos;
    }

    [HttpGet("queryByName")]
    public RoleVo QueryByName([FromQuery] string name)
    {
        return (from rolePo in _db.Role
                where rolePo.name == name
                select new RoleVo { Name = rolePo.name })
            .FirstOrDefault(new RoleVo());
    }
}