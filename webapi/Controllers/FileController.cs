using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.model.po;
using webapi.model.vo;
using webapi.util;
using MediaTypeHeaderValue = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace webapi.Controllers;

[Route("file")]
[Authorize]
public class FileController
{
    private AppDbContext _db;

    private string directory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar+"myupfiles"+ Path.DirectorySeparatorChar;

    public FileController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("upload")]
    public async Task<IResult> Upload([FromForm] IFormCollection formcollection,[FromForm]string tag)
    {
        if (formcollection.Count==0)
        {
            return Results.BadRequest("文件不能为空");
        }

        if (tag==null)
        {
            return Results.BadRequest("tag is not allow null");
        }

        var fileNameList = formcollection.Files.Select(e => e.FileName).ToList();
        var existFiles = _db.Resource.Where(e => tag == e.Tag && fileNameList.Contains(e.fileOrginname))
            .Select(e => e.fileOrginname).ToList();
        if (existFiles.Count != 0)
        {
            return Results.BadRequest($"文件已存在:{string.Join(",", existFiles)}");
        }

        List<ResourcePo> list = new List<ResourcePo>();
        var order = _db.Resource.Where(e => e.Tag == tag).OrderByDescending(e => e.Sort).AsEnumerable()
            .Select(e => e.Sort).FirstOrDefault(0);
        foreach (var file in formcollection.Files)
        {
            string contentType = file.ContentType;
            string fileOrginname = file.FileName; //新建文本文档.txt  原始的文件名称
            
            string fileExtention = Path.GetExtension(fileOrginname);
            string cdipath = Directory.GetCurrentDirectory();
          
            if (!Directory.Exists(directory))
            {
                 Directory.CreateDirectory(directory);
            }

            string fileupName = Guid.NewGuid().ToString("d") + fileExtention;   
            string upfilePath = Path.Combine(directory, fileupName);
            if (!File.Exists(upfilePath))
            {
                using var stream = File.Create(upfilePath);
            }

            double count = await UpLoadFileStreamHelper.UploadWriteFileAsync(file.OpenReadStream(), upfilePath);
            
            var resourcePo = new ResourcePo();

            resourcePo.fileOrginname = fileOrginname;
            resourcePo.contentType = contentType;
            resourcePo.fileExtention = fileExtention;
            resourcePo.fileupName = fileupName;
            resourcePo.Sort = ++order;
            resourcePo.Tag = tag;
            list.Add(resourcePo);
            // result.code = statuCode.success;
            // result.data = $"上传的文件大小为:{count}MB";
        }
        _db.Resource.AddRange(list);
        _db.SaveChanges();
        return Results.Ok("上传成功!");
    }

    [HttpGet("download/{filename}")]
    public async Task<FileContentResult> Download(string filename)
    {
        var resourcePo = _db.Resource.OrderByDescending(e=>e.CreateTime).FirstOrDefault(e => e.fileOrginname == filename);
        if (resourcePo==null)
        {
            throw new ArgumentException("文件不存在");
        }

        string path = directory + resourcePo.fileupName;
        var bytes = File.ReadAllBytes(path);
        var actionresult = new FileContentResult(bytes, new MediaTypeHeaderValue(resourcePo.contentType));


        actionresult.EnableRangeProcessing = true;
        //设定文件名称
        // actionresult.FileDownloadName = filename + "." + resourcePo.fileExtention;

        return actionresult;
    }

    [HttpGet("queryAll")]
    public List<ResourceVo> Query()
    {
        var resourcePos = _db.Resource.ToList();
        var results = resourcePos.Select(e =>
        {
            var type = e.contentType.Split("/").FirstOrDefault("null");
            return new ResourceVo
            {
                Name = e.fileOrginname,
                Type = type,
                Tag = e.Tag,
                Sort = e.Sort
            };
        }).ToList();

        return results;
    }

    [HttpGet("queryByTag")]
    public List<ResourceVo> QueryByTag([FromQuery]string tag)
    {
        var resourcePos = _db.Resource.Where(e => e.Tag == tag).OrderBy(e => e.Sort).ToList();
        
        var results = resourcePos.Select(e =>
        {
            var type = e.contentType.Split("/").FirstOrDefault("null");
            return new ResourceVo
            {
                Name = e.fileOrginname,
                Type = type,
                Tag = e.Tag,
                Sort = e.Sort
            };
        }).ToList();
        return results;
    }

    [HttpPost("modidy")]
    public IResult Modify([FromBody]ModifyResourceVo req)
    {
        var po = _db.Resource.FirstOrDefault(e => e.fileOrginname == req.name && e.Tag == req.tag);
        if (po==null)
        {
            return Results.BadRequest("文件不存在");
        }
        // 改名
        if (string.IsNullOrWhiteSpace(req.newName) && po.Sort==req.newSort)
        {
            po.fileOrginname = req.newName;
            _db.Resource.Update(po);
            _db.SaveChanges();
        }


        var sortList = _db.Resource.Where(e => po.Sort > req.newSort ? e.Sort <= po.Sort && e.Sort >= req.newSort : e.Sort >= po.Sort && e.Sort <= req.newSort)
            .ToList();
        if (sortList.Count == 0)
        {
            return Results.BadRequest("参数错误");
        }
        
        for (var i = 0; i < sortList.Count; i++)
        {
            if (req.name==sortList[i].fileOrginname)
            {
                sortList[i].Sort = req.newSort;
                if (!string.IsNullOrWhiteSpace(req.newName))
                {
                    sortList[i].fileOrginname = req.newName;
                }
            }else{
                if (po.Sort>req.newSort)
                {
                    sortList[i].Sort += 1;
                }
                else
                {
                    sortList[i].Sort -= 1;
                }
            }
        }
        _db.Resource.UpdateRange(sortList);
        _db.SaveChanges();
        return Results.Ok("排序成功");
    }
}