using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;

[Route("news")]
public class NewsController
{
    private AppDbContext _db;

    public NewsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("saveOrUpdate")]
    [Authorize]
    public IResult SaveOrUpdate([FromBody]NewsVo newsVo)
    {
        var newPo = _db.News.FirstOrDefault(e => e.title == newsVo.title);
        if (newPo == null)
        {
            var newsPo = new NewsPo()
            {
                title = newsVo.title,
                description = newsVo.description,
                content = newsVo.content,
                attr1 = newsVo.attr1,
                attr2 = newsVo.attr2,
                attr3 = newsVo.attr3,
            };
            _db.News.Add(newsPo);
        }
        else
        {
            newPo.title = newsVo.title;
            newPo.description = newsVo.description;
            newPo.content = newsVo.content;
            newPo.attr1 = newsVo.attr1;
            newPo.attr2 = newsVo.attr2;
            newPo.attr3 = newsVo.attr3;
            _db.News.Update(newPo);
        }
        _db.SaveChanges();
        return Results.Ok("ok");
    }

    [HttpPost("deleteByTitle")]
    [Authorize]
    public IResult DeleteByTitle([FromBody]NewsTitleVo newsVo)
    {
        var newPo = _db.News.FirstOrDefault(e => e.title == newsVo.title);
        if (newPo==null)
        {
            return Results.Ok("新闻动态不存在");
        }

        _db.News.Remove(newPo);
        _db.SaveChanges();
        return Results.Ok("操作成功");
    }

    [HttpGet("QueryAll")]
    public IResult QueryAll()
    {
        var newsPos = _db.News.ToList();
        List<NewsVo> arr = new List<NewsVo>();
        foreach (var newPo in newsPos)
        {
            var vo = new NewsVo()
            {
                title = newPo.title,
                description = newPo.description,
                content = newPo.content,
                attr1 = newPo.attr1,
                attr2 = newPo.attr2,
                attr3 = newPo.attr3,
            };
            arr.Add(vo);
        }
        return Results.Ok(arr);
    }


    [HttpPost("queryByTitle")]
    public IResult QueryByTitle([FromBody] NewsTitleVo newsVo)
    {
        var newPo = _db.News.FirstOrDefault(e => e.title == newsVo.title);
        if (newPo==null)
        {
            return Results.Ok("新闻动态不存在");
        }
        var vo = new NewsVo()
        {
            title = newPo.title,
            description = newPo.description,
            content = newPo.content,
            attr1 = newPo.attr1,
            attr2 = newPo.attr2,
            attr3 = newPo.attr3,
        };
        return Results.Ok(vo);
    }
}