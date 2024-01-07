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
        var po = new ResourcePo { name = resourceVo.Name };
        if (resourceVo.PermissionVos.Count != 0)
        {
            var list = _db.Permission.Where(e => resourceVo.PermissionVos.Contains(e.name)).ToList();
            if (resourceVo.PermissionVos.Count != list.Count) return Results.BadRequest("权限不存在");
            po.PermissionPos = list;
        }

        if (resourceVo.RoleVos.Count != 0)
        {
            var rolePos = _db.Role.Where(e => resourceVo.RoleVos.Contains(e.name)).ToList();
            if (rolePos.Count != resourceVo.RoleVos.Count) return Results.BadRequest("角色不存在");
            po.RolePos = rolePos;
        }

        _db.Resource.Add(po);
        _db.SaveChanges();
        return Results.Ok();
    }

    [HttpGet("query")]
    public List<ResourcePo> Query()
    {
        var resourcePos = _db.Resource.Include(e => e.PermissionPos).Include(e => e.RolePos).ToList();
        return resourcePos;
    }

    [HttpGet("queryByName")]
    public ResourcePo? QueryByName([FromQuery] string name)
    {
        var userVo = _db.Resource.Where(e => e.name == name).Include(e => e.PermissionPos).Include(e => e.RolePos)
            .FirstOrDefault();
        return userVo;
    }
}