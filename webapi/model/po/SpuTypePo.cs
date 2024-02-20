namespace webapi.model.po;

public class SpuTypePo: EntityPo
{
    public string name { get; set; }

    public string? description { set; get; }
    
    public List<SpuPo> spuPos { get; set; }

    public string? attr1 { set; get; }
    
    public string? attr2 { set; get; }
    
    public string? attr3 { set; get; }
    
}