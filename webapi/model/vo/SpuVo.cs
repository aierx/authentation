namespace webapi.model.vo;

public class SpuVo
{
    public Dictionary<string, string> name { get; set; }
    
    public long price { get; set; }

    public List<string> spuTypeList { get; set; }

    public Dictionary<string, string> descroption { get; set; }
    
    public string attr1 { set; get; }
    
    public string attr2 { set; get; }
    
    public string attr3 { set; get; }
}