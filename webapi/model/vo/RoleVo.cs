namespace webapi.model.vo;

public class RoleVo
{
    public long Id { get; set; }
    public string Name { get; set; }

    public List<PermissionVo> PermissionVos { get; set; }
}