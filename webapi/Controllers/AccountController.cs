using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;

[Route("account")]
[EnableCors("aaa")]
// [MinimumAgeAuthorize(10)]
public class AccountController : Controller
{
    private readonly AppDbContext _db;

    public AccountController(AppDbContext db)
    {
        _db = db;
    }


    [HttpPost("login")]
    public IResult login([FromBody] UserLoginVo userLoginVo)
    {
        var loginVoInDb = (from userPo in _db.User
            where userPo.name == userLoginVo.Name
            select new UserLoginVo { Name = userPo.name, Passwd = userPo.passwd }).FirstOrDefault();
        ;
        if (loginVoInDb == null || loginVoInDb.Passwd != userLoginVo.Passwd) return Results.BadRequest("用户不存在或密码错误");

        var po = _db.User.Where(e => e.name == userLoginVo.Name).Include(e => e.RolePos)
            .ThenInclude(e => e.PermissionPos).First();
        var identity = new ClaimsIdentity("Basic");
        foreach (RolePo rolePo in po.RolePos)
        {
            identity.AddClaim(new("Role",rolePo.name));            
        }
        HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        return Results.Ok("登入成功");
    }

    [HttpPost("logout")]
    public IResult logout()
    {
        HttpContext.SignOutAsync();
        return Results.Ok("退出登入");
    }
}