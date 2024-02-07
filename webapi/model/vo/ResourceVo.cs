namespace webapi.model.vo;

public class ResourceVo
{
    public string Name { get; set; } = null!;

    public string Type { get; set; }

    public string Tag { get; set; }
    
    public int? Sort { get; set; }
}