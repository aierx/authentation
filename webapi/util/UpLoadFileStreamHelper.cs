namespace webapi.util;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

public class UpLoadFileStreamHelper
{
    const int WRITE_FILE_SIZE = 1024 * 1024 * 2;

    /// <summary>
    /// 同步上传的方法WriteFile(Stream stream, string path)
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="path">物理路径</param>
    /// <returns></returns>
    public static double UploadWriteFile(Stream stream, string path)
    {
        try
        {
            double writeCount = 0;
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                       FileShare.ReadWrite, WRITE_FILE_SIZE))
            {
                Byte[] by = new byte[WRITE_FILE_SIZE];
                int readCount = 0;
                while ((readCount = stream.Read(by, 0, by.Length)) > 0)
                {
                    fileStream.Write(by, 0, readCount);
                    writeCount += readCount;
                }

                return Math.Round((writeCount * 1.0 / (1024 * 1024)), 2);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("发生异常：" + ex.Message);
        }
    }

    /// <summary>
    /// WriteFileAsync(Stream stream, string path)
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="path">物理路径</param>
    /// <returns></returns>
    public static async Task<double> UploadWriteFileAsync(Stream stream, string path)
    {
        try
        {
            double writeCount = 0;
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                       FileShare.ReadWrite, WRITE_FILE_SIZE))
            {
                Byte[] by = new byte[WRITE_FILE_SIZE];

                int readCount = 0;
                while ((readCount = await stream.ReadAsync(by, 0, by.Length)) > 0)
                {
                    await fileStream.WriteAsync(by, 0, readCount);
                    writeCount += readCount;
                }
            }

            return Math.Round((writeCount * 1.0 / (1024 * 1024)), 2);
        }
        catch (Exception ex)
        {
            throw new Exception("发生异常：" + ex.Message);
        }
    }

    /// <summary>
    /// 上传文件，需要自定义上传的路径
    /// </summary>
    /// <param name="file">文件接口对象</param>
    /// <param name="path">需要自定义上传的路径</param>
    /// <returns></returns>
    public static async Task<bool> UploadWriteFileAsync(IFormFile file, string path)
    {
        try
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                       FileShare.ReadWrite, WRITE_FILE_SIZE))
            {
                await file.CopyToAsync(fileStream);
                return true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("发生异常：" + ex.Message);
        }
    }


    /// <summary>
    /// 上传文件，配置信息从自定义的json文件中拿取
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static async Task<bool> UploadWriteFileAsync(IFormFile file)
    {
        try
        {
            if (file != null && file.Length > 0)
            {
                string contentType = file.ContentType;
                string fileOrginname = file.FileName; //新建文本文档.txt  原始的文件名称
                string fileExtention = Path.GetExtension(fileOrginname); //判断文件的格式是否正确
                string cdipath = Directory.GetCurrentDirectory();
                

                string fileupName = Guid.NewGuid().ToString("d") + fileExtention;
                string upfilePath = Path.Combine(cdipath, fileupName); //"/myupfiles/" 可以写入到配置文件中
                if (!File.Exists(upfilePath))
                {
                    using var Stream = File.Create(upfilePath);
                }

                using (FileStream fileStream = new FileStream(upfilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, WRITE_FILE_SIZE))
                {
                    await file.CopyToAsync(fileStream);
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception("发生异常：" + ex.Message);
        }
    }


    public static async Task<bool> UploadWriteFileByModelAsync(UpFileModel model)
    {
        try
        {
            var files = model.file.Files; // formcollection.Files;//formcollection.Count > 0 这样的判断为错误的
            if (files != null && files.Any())
            {
                var file = files[0];
                string contentType = file.ContentType;
                string fileOrginname = file.FileName; //新建文本文档.txt  原始的文件名称
                string fileExtention = Path.GetExtension(fileOrginname); //判断文件的格式是否正确
                string cdipath = Directory.GetCurrentDirectory();
                string fileupName = Guid.NewGuid().ToString("d") + fileExtention;
                string upfilePath = Path.Combine(cdipath + "/myupfiles/", fileupName); //可以写入到配置文件中
                if (!System.IO.File.Exists(upfilePath))
                {
                    using var Stream = System.IO.File.Create(upfilePath);
                }

                using (FileStream fileStream = new FileStream(upfilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                           FileShare.ReadWrite))
                {
                    using (Stream stream = file.OpenReadStream()) //理论上这个方法高效些
                    {
                        await stream.CopyToAsync(fileStream);
                        return true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("发生异常：" + ex.Message);
        }

        return false;
    }

    /*
     webapi 端的代码

    [HttpPost, Route("UpFile02")]
    public async Task<ApiResult> UpFileBymodel([FromForm]UpFileModel model)// 这里一定要加[FromForm]的特性，模型里面可以不用加
    {
        ApiResult result = new ApiResult();
        try
        {
            bool falg = await UpLoadFileStreamHelper.UploadFileByModel(model);
            result.code = falg ? statuCode.success : statuCode.fail;
            result.message = falg ? "上传成功" : "上传失败";
        }
        catch (Exception ex)
        {
            result.message = "上传异常,原因:" + ex.Message;
        }
        return result;
    }
     */
}

public class UpFileModel
{
    public IFormCollection file { get; set; }
}

