using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.model.po;

public class NewsPo: EntityPo
{
    public Dictionary<string,string> title { get; set; }
    public Dictionary<string,string> description { get; set; }
    public Dictionary<string,string> content { get; set; }
    public string attr1 { get; set; }
    public string attr2 { get; set; }
    public string attr3 { get; set; }
}