using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;

[Route("spu")]
public class SpuController
{
    private AppDbContext _db;


    public SpuController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("saveOrUpdate")]
    [Authorize]
    public IResult SaveOrUpdate([FromBody] SpuVo spu)
    {
        var spuPo = _db.Spu.FirstOrDefault(e => e.name == spu.name);
        List<SpuTypePo> arr = new List<SpuTypePo>();
        foreach (var s in spu.spuTypeList)
        {
            var spuTypePo = new SpuTypePo()
            {
                name = s
            };
            arr.Add(spuTypePo);
        }
        if (spuPo != null)
        {
            _db.Spu.Remove(spuPo);
        }

        spuPo = new SpuPo
        {
            name = spu.name,
            descroption = spu.descroption,
            spuTypeList = arr,
            price = spu.price,
            attr1 = spu.attr1,
            attr2 = spu.attr2,
            attr3 = spu.attr3,
        };
        _db.Spu.Add(spuPo);
        _db.SaveChanges();
        return Results.Ok("ok");
    }

    [HttpPost("deleteByName")]
    [Authorize]
    public IResult DeleteByName([FromBody] SpuNameVo spuVo)
    {
        var spuPo = _db.Spu.FirstOrDefault(e => e.name == spuVo.name);
        if (spuPo == null)
        {
            return Results.Ok("产品不存在");
        }

        _db.Spu.Remove(spuPo);
        _db.SaveChanges();
        return Results.Ok("操作成功");
    }

    [HttpGet("queryAll")]
    public IResult QueryAll()
    {
        var spuPos = _db.Spu.Include(spuPo => spuPo.spuTypeList).ToList();
        List<SpuVo> arr = new List<SpuVo>();
        foreach (var spuPo in spuPos)
        {
            var vo = new SpuVo()
            {
                name = spuPo.name,
                price = spuPo.price,
                spuTypeList = spuPo.spuTypeList.Select(e => e.name).ToList(),
                descroption = spuPo.descroption,
                attr1 = spuPo.attr1,
                attr2 = spuPo.attr2,
                attr3 = spuPo.attr3,
            };
            arr.Add(vo);
        }

        return Results.Ok(arr);
    }


    [HttpPost("queryByName")]
    public IResult QueryByName([FromBody] SpuNameVo spuVo)
    {
        var spuPo = _db.Spu.Include(spuPo => spuPo.spuTypeList).FirstOrDefault(e => e.name == spuVo.name);
        if (spuPo == null)
        {
            return Results.Ok("产品不存在");
        }

        var vo = new SpuVo()
        {
            name = spuPo.name,
            price = spuPo.price,
            spuTypeList = spuPo.spuTypeList.Select(e => e.name).ToList(),
            attr1 = spuPo.attr1,
            attr2 = spuPo.attr2,
            attr3 = spuPo.attr3,
        };
        return Results.Ok(vo);
    }

    [HttpPost("queryByType")]
    public IResult QueryByType([FromQuery] string type)
    {
        var typePo = _db.SpuType.FirstOrDefault(e => e.name == type);
        if (typePo == null)
        {
            return Results.BadRequest("指定类型不存在");
        }

        var spuPos = _db.Spu.Include(spuPo => spuPo.spuTypeList).ToList();

        spuPos = spuPos.Where(e => e.spuTypeList.Select(c=>c.name).ToList().Contains(type)).ToList();
        
        List<SpuVo> arr = new List<SpuVo>();
        foreach (var spuPo in spuPos)
        {
            var vo = new SpuVo()
            {
                name = spuPo.name,
                price = spuPo.price,
                spuTypeList = spuPo.spuTypeList.Select(e => e.name).ToList(),
                attr1 = spuPo.attr1,
                attr2 = spuPo.attr2,
                attr3 = spuPo.attr3,
            };
            arr.Add(vo);
        }
        return Results.Ok(arr);
    }
}