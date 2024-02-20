using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.model.po;

public class SpuPo : EntityPo
{
    public Dictionary<string, string> name { get; set; }
    
    public long price { get; set; }

    public List< SpuTypePo>? spuTypeList { get; set; }

    public Dictionary<string, string>? descroption { get; set; }
    
    public string? attr1 { set; get; }
    
    public string? attr2 { set; get; }
    
    public string? attr3 { set; get; }
}