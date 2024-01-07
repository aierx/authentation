using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;
[Route("permission")]
[EnableCors("aaa")]
public class PermissionController
{
    private readonly AppDbContext _db;

    public PermissionController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("add")]
    public IResult Add([FromBody] PermissionVo permissionVo)
    {
        if (_db.Permission.Any(e => e.name == permissionVo.Name))
        {
            return Results.BadRequest("权限已经存在");
        }
        var userPo = new PermissionPo { name = permissionVo.Name};
        _db.Permission.Add(userPo);
        _db.SaveChanges();
        return Results.Ok();
    }
    
    [HttpGet("query")]
    [Authorize]
    public List<UserVo> Query()
    {
        var userVos = (from t in _db.Permission select new UserVo() { Name = t.name ,Id = t.Id}).ToList();
        return  userVos;
    }

    [HttpGet("queryByName")]
    public UserVo QueryByName([FromQuery] string name)
    {
        var userVo = (from userPo in _db.User where userPo.name == name select new UserVo() { Name = userPo.name, Id = userPo.Id })
            .First();
        return userVo;
    }
}