using Microsoft.EntityFrameworkCore;

namespace webapi.model.po;

[Index("Sort")]
public class ResourcePo : EntityPo
{
    public string fileOrginname { get; set; }
    
    public string fileupName { get; set; }

    public string contentType { set; get; }

    public string fileExtention { set; get; }
    
    public string Tag { get; set; }

    public int? Sort { get; set; }
}