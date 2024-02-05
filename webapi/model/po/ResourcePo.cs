using Microsoft.EntityFrameworkCore;

namespace webapi.model.po;

public class ResourcePo : EntityPo
{
    public string fileOrginname { get; set; }
    
    public string fileupName { get; set; }

    public string contentType { set; get; }

    public string fileExtention { set; get; }
}