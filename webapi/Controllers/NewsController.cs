using Microsoft.AspNetCore.Mvc;
using webapi.model.po;
using webapi.model.vo;

namespace webapi.Controllers;

public class NewsController
{
    private AppDbContext _db;

    public NewsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("save")]
    public IResult save([FromBody]NewsVo newsVo)
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
        _db.SaveChanges();
        return Results.Ok("ok");
    }
}