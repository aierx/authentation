using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using webapi.model.po;
using webapi.util;

namespace webapi.Controllers;

[Route("file")]
public class FileController
{
    private AppDbContext _db;

    private string directory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar+"myupfiles"+ Path.DirectorySeparatorChar;

    public FileController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("upload")]
    public async Task<IResult> Upload([FromForm] IFormCollection formcollection)
    {
        var files = formcollection.Files;
        if (files != null || files.Any())
        {
            var file = files[0];
            string contentType = file.ContentType;
            string fileOrginname = file.FileName; //新建文本文档.txt  原始的文件名称

            // if (_db.Resource.Any(e => e.fileOrginname==fileOrginname))
            // {
            //     return Results.BadRequest("文件已存在");
            // }
            
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
            // result.code = statuCode.success;
            // result.data = $"上传的文件大小为:{count}MB";
            _db.Resource.Add(resourcePo);
            _db.SaveChanges();
        }
        return Results.Ok("上传成功!");
    }

    [HttpGet("download")]
    public async Task<FileStreamResult> download([FromQuery]string filename)
    {
        var resourcePo = _db.Resource.OrderByDescending(e=>e.CreateTime).FirstOrDefault(e => e.fileOrginname == filename);
        if (resourcePo==null)
        {
            throw new ArgumentException("文件不存在");
        }

        string path = directory + resourcePo.fileupName;
        var stream = File.Open(path,FileMode.Open);
        //设定文件类型
        var actionresult = new FileStreamResult(stream, new MediaTypeHeaderValue("image/png"));
        //设定文件名称
        actionresult.FileDownloadName = filename+"."+resourcePo.fileExtention;
        return actionresult;
    }
}