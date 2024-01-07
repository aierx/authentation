namespace webapi.model.vo;

public class ResourceVo
{
    public string Name { get; set; } = null!;

    public List<string> RoleVos { get; set; } = null!;

    public List<string> PermissionVos { get; set; } = null!;
}