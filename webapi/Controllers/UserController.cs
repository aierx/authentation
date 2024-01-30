using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.model.po;
using webapi.model.vo;
using webapi.util;

namespace webapi.Controllers;

[Route("user")]
// [EnableCors("aaa")]
public class UserController
{
    private readonly AppDbContext _db;

    private static IHashPassword hashPassword = new SHA512HashPassword();

    public UserController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("add")]
    public IResult Add([FromBody] UserVo userVo)
    {
        if (_db.User.Any(e => e.name==userVo.Name))
        {
            return Results.Problem("用户已存在");
        }

        if (userVo.RoleVos.Count != 0 && _db.Role.Count(e => userVo.RoleVos.Contains(e.name)) != userVo.RoleVos.Count())
        {
            return Results.Problem("指定角色不存在");
        }

        var salt = hashPassword.GenerateSalt();
        string hashPassWord = hashPassword.Generate(salt,userVo.Password);
        var userPo = new UserPo { name = userVo.Name,passwd = hashPassWord,salt = salt};
        
        if (userVo.RoleVos.Count != 0)
        {
            var list = _db.Role.Where(e => userVo.RoleVos.Contains(e.name)).ToList();
            userPo.RolePos = list;
        }

        _db.User.Add(userPo);
        _db.SaveChanges();
        return Results.Ok("添加用户成功");
    }

    [HttpPost("modifyPassWord")]
    public IResult ModifyPassWord([FromBody] ModifyPassWordVO modifyPassWordVo)
    {
        var userPo = _db.User.Where(e => e.name == modifyPassWordVo.Name).Include(e => e.RolePos).FirstOrDefault();
        if (userPo == null)
        {
            return Results.Problem("用户不存在");
        }
        if (!hashPassword.Validate(modifyPassWordVo.OldPassWord,userPo.passwd))
        {
            return Results.Problem("密码不正确");
        }
        var salt = hashPassword.GenerateSalt();
        userPo.passwd = hashPassword.Generate(salt, modifyPassWordVo.NewPassWord);
        userPo.salt = salt;
        _db.User.Update(userPo);
        _db.SaveChanges();
        return Results.Ok("修改密码成功");
    }

    [HttpPost("/deleteByName")]
    public IResult DeleteByName([FromBody] string name)
    {
        var userPo = _db.User.FirstOrDefault(e=>e.name==name);
        if (userPo==null)
        {
            return Results.Problem("用户不存在");
        }

        _db.User.Remove(userPo);
        _db.SaveChanges();
        return Results.Ok("删除用户成功");
    }

    [HttpGet("queryAll")]
    public List<UserVo> Query()
    {
        var userVos = new List<UserVo>();
        foreach (var userPo in _db.User.Include(po => po.RolePos))
        {
            var userVo = new UserVo
            {
                Name = userPo.name,
                RoleVos = userPo.RolePos.Select(e => e.name).ToList()
            };
            userVos.Add(userVo);
        }
        return userVos;
    }

    [HttpGet("queryByName")]
    public IResult QueryByName([FromQuery] string name)
    {
        var userVo = (from userPo in _db.User
                where userPo.name == name
                select new UserVo { Name = userPo.name })
            .FirstOrDefault();
        if (userVo == null) return Results.BadRequest("用户不存在");
        return Results.Ok(userVo);
    }
}