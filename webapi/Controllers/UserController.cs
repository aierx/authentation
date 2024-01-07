using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;
[Route("user")]
[EnableCors("aaa")]

public class UserController
{

    private AppDbContext _db;

    public UserController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("add")]
    public IResult add([FromBody] PermissionVo permissionVo)
    {
        UserPo userPo = new UserPo() { name = permissionVo.Name };
        _db.User.Add(userPo);
        _db.SaveChanges();
        return Results.Ok();
    }
    
    [HttpGet("query")]
    [Authorize]
    public List<PermissionVo> query()
    {
        var userVos = (from userPo in _db.User select new PermissionVo() { Name = userPo.name ,Id = userPo.Id}).ToList();
        return  userVos;
    }

    [HttpGet("queryByName")]
    public PermissionVo queryByName([FromQuery] string name)
    {
        var userVo = (from userPo in _db.User where userPo.name == name select new PermissionVo() { Name = userPo.name, Id = userPo.Id })
            .First();
        return userVo;
    }
}