using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;


[Route("account")]
[EnableCors("aaa")]

public class AccountController :Controller
{

    private AppDbContext _db;

    public AccountController(AppDbContext db)
    {
        _db = db;
    }


    [HttpPost("login")]
    public IResult login([FromBody] UserLoginVo userLoginVo)
    {
        var loginVoInDb = (from userPo in _db.User
            where userPo.name == userLoginVo.Name
            select new UserLoginVo() { Name = userPo.name, Passwd = userPo.passwd }).FirstOrDefault();;
        if (loginVoInDb==null || loginVoInDb.Passwd!=userLoginVo.Passwd)
        {
           
            return  Results.BadRequest("用户不存在或密码错误");
        }

        UserPo po = _db.User.Where(e => e.name == userLoginVo.Name).Include(e => e.RolePos).ThenInclude(e => e.PermissionPos).First();

        var claimsIdentity = new ClaimsIdentity(new Claim[] { new Claim("Role", Json(po).ToString()!)}, "Basic");
        HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
        return Results.Ok("登入成功");
    }
    
    [HttpPost("logout")]
    public IResult logout()
    {
        HttpContext.SignOutAsync();
        return Results.Ok("退出登入");
    }
    
}