using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.model.po;

public class EntityPo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [JsonIgnore] public DateTime CreateTime { get; set; }

    [JsonIgnore] public DateTime ModifyTime { get; set; }

    [JsonIgnore] public string? Creator { get; set; }

    [JsonIgnore] public string? modifer { get; set; }
}