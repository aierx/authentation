using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;

[Route("user")]
[EnableCors("aaa")]
public class UserController
{
    private readonly AppDbContext _db;

    public UserController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("add")]
    public IResult Add([FromBody] UserVo userVo)
    {
        var userPo = new UserPo { name = userVo.Name };
        if (userVo.RoleVos.Count!=0)
        {
           var list = _db.Role.Where(e => userVo.RoleVos.Contains(e.name)).ToList();
           userPo.RolePos = list;
        }
        _db.User.Add(userPo);
        _db.SaveChanges();
        return Results.Ok();
    }

    [HttpPost("update")]
    public IResult Update([FromBody] UserVo userVo)
    {
        var userPo = _db.User.Where(e => e.name == userVo.Name).Include(e => e.RolePos).FirstOrDefault();
        if (userPo==null)
        {
            return Results.BadRequest("用户不存在");
        }
        if (userVo.RoleVos.Count!=0)
        {
            var list = _db.Role.Where(e => userVo.RoleVos.Contains(e.name)).ToList();
            userPo.RolePos = list;
        }else
        {
            return Results.BadRequest("没有改动");
        }

        _db.User.Update(userPo);
        _db.SaveChanges();
        return Results.Ok();
    }
    

    [HttpGet("query")]
    public List<UserVo> Query()
    {
        var userVos = (from userPo in _db.User select new UserVo { Name = userPo.name, Id = userPo.Id }).ToList();
        return userVos;
    }

    [HttpGet("queryByName")]
    public IResult QueryByName([FromQuery] string name)
    {
        var userVo = (from userPo in _db.User
                where userPo.name == name
                select new UserVo { Name = userPo.name, Id = userPo.Id })
            .FirstOrDefault();
        if (userVo==null)
        {
            return Results.BadRequest("用户不存在");
        }
        return Results.Ok(userVo);
    }
}