using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.model.vo;
using webapi.util;

namespace webapi.Controllers;

[Route("account")]
public class AccountController : Controller
{
    private readonly AppDbContext _db;
    
    private static IHashPassword hashPassword = new SHA512HashPassword();


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

        if (loginVoInDb == null || !hashPassword.Validate(userLoginVo.Passwd,loginVoInDb.Passwd) )
        {
            return Results.BadRequest("用户不存在或密码错误");
        }

        var poList = _db.User.Where(e => e.name == userLoginVo.Name).Include(e => e.RolePos).First();
        var identity = new ClaimsIdentity("Basic");
        foreach (var rolePo in poList.RolePos)
        {
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, rolePo.name));
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