using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.model.po;
using webapi.model.vo;
using webapi.util;

namespace webapi.Controllers;

[Route("user")]
[Authorize(Roles = "admin")]
public class UserController
{
    private readonly AppDbContext _db;

    private static IHashPassword hashPassword = new SHA512HashPassword();

    public UserController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("add")]
    [Authorize(Roles = "admin")]
    public IResult Add([FromBody] AddUserVo addUserVo)
    {
        if (_db.User.Any(e => e.name==addUserVo.Name))
        {
            return Results.Problem("用户已存在");
        }

        if (addUserVo.RoleVos.Count != 0 && _db.Role.Count(e => addUserVo.RoleVos.Contains(e.name)) != addUserVo.RoleVos.Count())
        {
            return Results.Problem("指定角色不存在");
        }

        var salt = hashPassword.GenerateSalt();
        string hashPassWord = hashPassword.Generate(salt,addUserVo.Password);
        var userPo = new UserPo { name = addUserVo.Name,passwd = hashPassWord,salt = salt};
        
        if (addUserVo.RoleVos.Count != 0)
        {
            var list = _db.Role.Where(e => addUserVo.RoleVos.Contains(e.name)).ToList();
            userPo.RolePos = list;
        }

        _db.User.Add(userPo);
        _db.SaveChanges();
        return Results.Ok("添加用户成功");
    }

    [HttpPost("modifyRole")]
    [Authorize(Roles = "admin")]
    public IResult ModifyRole([FromBody] ModifyRoleVo modifyRoleVo)
    {
        var userPo = _db.User.Include(e=>e.RolePos).FirstOrDefault(e => e.name == modifyRoleVo.Name);
        if (userPo==null)
        {
            return Results.Problem("用户不存在");
        }
        
        var rolePos = _db.Role.Where(e => modifyRoleVo.RoleVos.Contains(e.name)).ToList();

        if (rolePos.Count!=modifyRoleVo.RoleVos.Count)
        {
            var dbRole = rolePos.Select(e=>e.name).ToList();
            modifyRoleVo.RoleVos.RemoveAll(e => dbRole.Contains(e));
            var hint = string.Join(",",modifyRoleVo.RoleVos);
            return Results.Problem($"以下角色不存在：{hint}");
        }

        userPo.RolePos.RemoveAll(_ => true);
        userPo.RolePos.AddRange(rolePos);
        _db.User.Update(userPo);
        _db.SaveChanges();
        return Results.Ok("修改用户角色成功");
    }

    [HttpPost("deleteByName")]
    [Authorize(Roles = "admin")]
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


    [HttpGet("queryAll")]
    public IResult Query()
    {
        var userVos = _db.User.Include(po => po.RolePos)
            .Select(userPo => new UserVo { Name = userPo.name, RoleVos = userPo.RolePos.Select(e => e.name).ToList() })
            .ToList();
        return Results.Ok(userVos);
    }

    [HttpGet("queryByName")]
    public IResult QueryByName([FromQuery] string name)
    {
        var userViewVo = _db.User.Where(e => e.name == name).Include(e => e.RolePos).Select(e => new UserViewVO()
        {
            Name = e.name,
            RoleVos = e.RolePos.Select(e => e.name).ToList()
        }).FirstOrDefault();
        return userViewVo==null ? Results.Problem("用户不存在") : Results.Ok(userViewVo);
    }
}