using System.Text.Json.Serialization;

namespace webapi.model.po;

public class EntityPo
{
    public long Id { get; set; }

    [JsonIgnore] public DateTime CreateTime { get; set; }

    [JsonIgnore] public DateTime ModifyTime { get; set; }

    [JsonIgnore] public string? Creator { get; set; }

    [JsonIgnore] public string? modifer { get; set; }
}