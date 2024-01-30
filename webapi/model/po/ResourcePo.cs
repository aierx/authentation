using Microsoft.EntityFrameworkCore;

namespace webapi.model.po;

public class ResourcePo : EntityPo
{
    public string fileName { get; set; }

    public string suffix { set; get; }
    
    public string type { set; get; }
    
}