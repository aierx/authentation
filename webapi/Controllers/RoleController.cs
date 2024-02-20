using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;

[Route("role")]
[Authorize(Roles = "admin")]
public class RoleController
{
    private readonly AppDbContext _db;

    public RoleController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("add")]
    [Authorize(Roles = "admin")]
    public IResult Add([FromBody] RoleVo roleVo)
    {
        if (_db.Role.Any(e => e.name == roleVo.Name))
        {
            return Results.Problem("角色已存在");
        }

        _db.Role.Add(new RolePo { name = roleVo.Name });
        _db.SaveChanges();
        return Results.Ok("添加角色成功");
    }

    [HttpPost("deleteByName")]
    [Authorize(Roles = "admin")]
    public IResult DeleteByName([FromBody] string name)
    {
        var rolePo = _db.Role.FirstOrDefault(e => e.name == name);
        if (rolePo == null)
        {
            return Results.Problem("角色不存在");
        }
        _db.Role.Remove(rolePo);
        _db.SaveChanges();
        return Results.Ok("删除成功");
    }

    [HttpGet("queryAll")]
    public IResult Query()
    {
        var userVos = (from rolePo in _db.Role select new RoleVo { Name = rolePo.name }).ToList();
        return Results.Ok(userVos);
    }

    [HttpGet("queryByName")]
    public IResult QueryByName([FromQuery] string name)
    {
        return Results.Ok((from rolePo in _db.Role
                where rolePo.name == name
                select new RoleVo { Name = rolePo.name })
            .FirstOrDefault(new RoleVo()));
    }
}