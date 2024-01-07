namespace webapi.model.po;

public class PermissionPo : EntityPo
{
    public string? name { get; set; }
    public List<RolePo>? RolePos { get; set; }
}